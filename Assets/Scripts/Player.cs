using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float m_speed = 0.0f;

    [SerializeField] private AudioSource m_audioSource = null;

    [SerializeField] private Weapon_Default m_prefab_weapon_default = null;
    [SerializeField] private Weapon_ShotGun m_prefab_weapon_shotGun = null;
    [SerializeField] private Weapon_LaserGun m_prefab_weapon_laserGun = null;

    private Weapon_Base[] m_weaponArr = new Weapon_Base[5];
    private Weapon_Base m_currWeapon = null;    

    private GameController s_gameController = null;
    private InventoryPanel s_inventoryPanel = null;
    private GameplayPanel s_gameplayPanel = null;





    protected override void Start()
    {
        m_weaponArr[0] = m_prefab_weapon_default;
        m_currWeapon = m_prefab_weapon_default;

        s_gameController = GameController.Instance;

        s_gameplayPanel = GameplayPanel.Instance;
        s_gameplayPanel.UpdateWeaponTypeInfo(m_prefab_weapon_default.GetType());

        s_inventoryPanel = InventoryPanel.Instance;
        s_inventoryPanel.InitCmd_SelectWeapon(new Command_SelectWeapon<Player>(this, (r,v)=> r.SelectWeapon(v)));
        s_inventoryPanel.Hide();
    }


    protected override void Update()
    {
        PlayerControl();

        ShootBullet();
    }


    protected void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Bonus bonus = i_collider.GetComponent<Enemy_Bonus>();
        Enemy_Laser laser = i_collider.GetComponent<Enemy_Laser>();

        double currentTime = Time.time;
        if (bonus != null)
        {
            OnPickShotGun();
        }
        else if (laser != null)
        {
            OnPickLaserGun();
        }
    }


    private void PlayerControl()
    {
        if (s_gameController.m_isSelectingWeapon == false)
        {
            Vector2 direction = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1;
            }

            if (Input.GetKey(KeyCode.W)) 
            {
                direction.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.y = -1;
            }

            transform.Translate(direction * m_speed * Time.deltaTime);
        }

        if (transform.position.x < -1 * s_gameController.Boundary_H)
            transform.position = new Vector3(s_gameController.Boundary_H - 0.5f, transform.position.y, 0);
        else if (transform.position.x > s_gameController.Boundary_H)
            transform.position = new Vector3(-1 * s_gameController.Boundary_H + 0.5f, transform.position.y, 0);

        if (transform.position.y < -1 * s_gameController.Boundary_V)
            transform.position = new Vector3(transform.position.x, s_gameController.Boundary_V - 0.5f, 0);
        else if (transform.position.y > s_gameController.Boundary_V)
            transform.position = new Vector3(transform.position.x, -1 * s_gameController.Boundary_V + 0.5f, 0);
    }


    private void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            //m_prefab_weapon_default.Fire();
            m_currWeapon.Fire();
            s_gameplayPanel.UpdateAmmoInfo(m_currWeapon.GetAmmo());
        }   
    }


    private void OnPickShotGun()
    {
        for (int i = 1; i < m_weaponArr.Length; i++)
        {
            if (m_weaponArr[i] == null)
            {
                Command_OutOfAmmo<Player> cmd = new Command_OutOfAmmo<Player>(this, (r, v) => r.OnOutOfAmmo(v));
                Weapon_ShotGun shotGun = Instantiate(m_prefab_weapon_shotGun, transform.position, transform.rotation, transform);
                shotGun.InitCmd_OnOutOfAmmo(cmd);
                m_weaponArr[i] = shotGun;
                s_inventoryPanel.UpdateContent(i, typeof(Weapon_ShotGun));
                return;
            }
        }
    }


    private void OnPickLaserGun()
    {
        for (int i = 1; i < m_weaponArr.Length; i++)
        {
            if (m_weaponArr[i] == null)
            {
                Command_OutOfAmmo<Player> cmd = new Command_OutOfAmmo<Player>(this, (r, v) => r.OnOutOfAmmo(v));
                Weapon_LaserGun laserGun = Instantiate(m_prefab_weapon_laserGun, transform.position, transform.rotation, transform);
                laserGun.InitCmd_OnOutOfAmmo(cmd);
                m_weaponArr[i] = laserGun;
                s_inventoryPanel.UpdateContent(i, typeof(Weapon_LaserGun));
                return;
            }
        }
    }


    private void OnOutOfAmmo(Weapon_Base i_weapon)
    {
        for (int i  = 1; i < m_weaponArr.Length; i++) 
        {
            Weapon_Base weapon = m_weaponArr[i];
            if (i_weapon == weapon)
            {
                s_inventoryPanel.UpdateContent(i, null);
                Destroy(weapon.gameObject);
                m_weaponArr[i] = null;
                break;
            }
        }

        m_currWeapon = m_prefab_weapon_default;
    }


    private void SelectWeapon(int i_idx)
    {
        m_currWeapon = m_weaponArr[i_idx];
        if (m_currWeapon == null)
            m_currWeapon = m_prefab_weapon_default;

        s_gameplayPanel.UpdateWeaponTypeInfo(m_currWeapon.GetType());
        s_gameplayPanel.UpdateAmmoInfo(m_currWeapon.GetAmmo());
    }
}

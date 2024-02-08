using System.Collections.Generic;
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

    private List<Bullet> m_bulletList = new List<Bullet>();

    private GameController s_gameController = null;
    private InventoryPanel s_inventoryPanel = null;
    private GameplayPanel s_gameplayPanel = null;


    public List<Bullet> PlayerBullets {  get { return m_bulletList; } }


    protected override void Start()
    {
        Init();
    }


    protected override void Update()
    {
        PlayerControl();

        Shoot();
    }


    protected override void OnDestroy() { }


    protected void OnTriggerEnter2D(Collider2D i_collider)
    {
        Bonus_Shotgun shotgun = i_collider.GetComponent<Bonus_Shotgun>();
        Bonus_Laser laser = i_collider.GetComponent<Bonus_Laser>();

        if (shotgun != null)
            OnPickWeapon(typeof(Weapon_ShotGun));
        else if (laser != null)
            OnPickWeapon(typeof(Weapon_LaserGun));
    }


    private void Init()
    {
        m_weaponArr[0] = m_prefab_weapon_default;
        m_currWeapon = m_prefab_weapon_default;

        s_gameController = GameController.Instance;

        s_gameplayPanel = GameplayPanel.Instance;
        s_gameplayPanel.UpdateWeaponTypeInfo(m_prefab_weapon_default.GetType());

        s_inventoryPanel = InventoryPanel.Instance;
        s_inventoryPanel.InitCmd_SelectWeapon(new Command_SelectWeapon<Player>(this, (r, v) => r.SelectWeapon(v)));
        s_inventoryPanel.Hide();
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

        if (transform.position.x < s_gameController.DownLeft.x)
            transform.position = new Vector3(s_gameController.TopRight.x - 0.5f, transform.position.y, 0);
        else if (transform.position.x > s_gameController.TopRight.x)
            transform.position = new Vector3(s_gameController.DownLeft.x + 0.5f, transform.position.y, 0);

        if (transform.position.y < s_gameController.DownLeft.y)
            transform.position = new Vector3(transform.position.x, s_gameController.TopRight.y - 0.5f, 0);
        else if (transform.position.y > s_gameController.TopRight.y)
            transform.position = new Vector3(transform.position.x, s_gameController.DownLeft.y + 0.5f, 0);
    }


    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            m_currWeapon.Fire();
            s_gameplayPanel.UpdateAmmoInfo(m_currWeapon.GetAmmo());
        }   
    }


    private void OnPickWeapon(System.Type i_weaponType)
    {
        for (int i = 1; i < m_weaponArr.Length; i++)
        {
            if (m_weaponArr[i] == null)
            {
                Weapon_Base weapon = null;
                if (i_weaponType == typeof(Weapon_LaserGun))
                {
                    weapon = Instantiate(m_prefab_weapon_shotGun, transform.position, transform.rotation, transform);
                    s_inventoryPanel.UpdateContent(i, typeof(Weapon_ShotGun));
                }
                else if (i_weaponType == typeof(Weapon_ShotGun))
                {
                    weapon = Instantiate(m_prefab_weapon_laserGun, transform.position, transform.rotation, transform);
                    s_inventoryPanel.UpdateContent(i, typeof(Weapon_LaserGun));

                }

                Command_OutOfAmmo<Player> cmd = new Command_OutOfAmmo<Player>(this, (r, v) => r.OnOutOfAmmo(v));
                weapon.InitCmd_OnOutOfAmmo(cmd);
                m_weaponArr[i] = weapon;
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
        s_gameplayPanel.UpdateWeaponTypeInfo(m_currWeapon.GetType());
        s_gameplayPanel.UpdateAmmoInfo(m_currWeapon.GetAmmo());
    }


    private void SelectWeapon(int i_idx)
    {
        m_currWeapon = m_weaponArr[i_idx];
        if (m_currWeapon == null)
            m_currWeapon = m_prefab_weapon_default;

        s_gameplayPanel.UpdateWeaponTypeInfo(m_currWeapon.GetType());
        s_gameplayPanel.UpdateAmmoInfo(m_currWeapon.GetAmmo());
    }


    public void RegisterBullet(Bullet i_bullet)
    {
        m_bulletList.Add(i_bullet);
    }


    public void DeregisterBullet(Bullet i_bullet)
    {
        m_bulletList.Remove(i_bullet);
    }

}

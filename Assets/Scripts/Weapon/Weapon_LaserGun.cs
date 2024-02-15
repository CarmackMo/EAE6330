using UnityEngine;


public class Weapon_LaserGun : Weapon_Base
{

    [SerializeField] private float m_ammoCooldown = 0.0f;
    private float m_lastUpdateAmmoTime = 0.0f;

    public override void Init() { }



    protected override void ShootBullets()
    {
        m_bulletPrefab.gameObject.SetActive(true);
    }


    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            m_bulletPrefab.gameObject.SetActive(false);
    }


    protected override void UpdateAmmo()
    {
        float currTime = Time.time;
        if (currTime - m_lastUpdateAmmoTime >= m_ammoCooldown)
        {
            m_ammo--;
            m_lastUpdateAmmoTime = currTime;
        }

        if (m_ammo == 0)
        {
            OnOutOfAmmo();
        }
    }

}

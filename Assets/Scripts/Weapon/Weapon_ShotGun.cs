using UnityEngine;


public class Weapon_ShotGun : Weapon_Base
{

    public override void Init() { }


    protected override void ShootBullets()
    {
        Bullet newBullet_1 = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
        newBullet_1.Velocity = new Vector2(0, 5);

        Bullet newBullet_2 = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
        newBullet_2.Velocity = new Vector2(4, 3);

        Bullet newBullet_3 = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
        newBullet_3.Velocity = new Vector2(-4, 3);

        Player.Instance.RegisterBullet(newBullet_1);
        Player.Instance.RegisterBullet(newBullet_2);
        Player.Instance.RegisterBullet(newBullet_3);
    }


    protected override void UpdateAmmo()
    {
        m_ammo--;

        if (m_ammo == 0)
        {
            OnOutOfAmmo();
        }
    }

}

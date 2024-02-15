using UnityEngine;


public class Weapon_Default : Weapon_Base
{

    public override void Init() { }


    protected override void ShootBullets()
    {
        Bullet newBullet = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
        newBullet.Velocity = new Vector2(0, 5);

        m_player.RegisterBullet(newBullet);
    }


    protected override void UpdateAmmo()
    {
        
    }

}

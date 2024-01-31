using UnityEngine;


public class Weapon_Default : Weapon_Base
{

    public override void Init() { }


    protected override void ShootBullets()
    {
        Bullet newBullet = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
        newBullet.Speed = new Vector2(0, 5);
    }


    protected override void UpdateAmmo()
    {
        
    }

}

using UnityEngine;


public class Bullet_Player_Default : Bullet
{
    protected override void CleanUp()
    {
        if (transform.position.y > m_cleanUpThreshold)
        {
            Player.Instance.DeregisterBullet(this);
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Rock rock = i_collider.gameObject.GetComponent<Enemy_Rock>();
        Enemy_Boss boss = i_collider.gameObject.GetComponent<Enemy_Boss>();
        Enemy_Alien alien = i_collider.gameObject.GetComponent<Enemy_Alien>();
        
        Enemy_Boss_Shield shield = i_collider.gameObject.GetComponent<Enemy_Boss_Shield>();

        Bullet_Enemy bullet = i_collider.gameObject.GetComponent<Bullet_Enemy>();


        if (rock != null || alien != null || bullet != null || boss != null)
        {
            Player.Instance.DeregisterBullet(this);
            Destroy (gameObject);
        }
        if (shield != null)
        {
            Player.Instance.DeregisterBullet(this);
            Destroy(gameObject);
        }
        else
            return;
    }

}

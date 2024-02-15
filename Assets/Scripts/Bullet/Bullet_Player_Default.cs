using UnityEngine;


public class Bullet_Player_Default : Bullet
{
    protected override void CleanUp()
    {
        Vector2 pos = transform.position;
        if (pos.x < m_downLeft.x - 2.0f || pos.x > m_topRight.x + 2.0f ||
            pos.y < m_downLeft.y - 2.0f || pos.y > m_topRight.y + 2.0f)
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

        Bullet_Enemy_Default bullet = i_collider.gameObject.GetComponent<Bullet_Enemy_Default>();


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

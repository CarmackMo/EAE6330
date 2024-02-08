using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player_Default : Bullet
{
    protected override void CleanUp()
    {
        if (transform.position.y > m_cleanUpThreshold)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Rock rock = i_collider.gameObject.GetComponent<Enemy_Rock>();
        Enemy_Alien alien = i_collider.gameObject.GetComponent<Enemy_Alien>();
        Bullet_Enemy bullet = i_collider.gameObject.GetComponent<Bullet_Enemy>();

        if (rock != null || alien != null || bullet != null) 
            Destroy (gameObject);
        else
            return;
    }

}

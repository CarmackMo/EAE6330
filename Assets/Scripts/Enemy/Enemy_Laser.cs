using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : Enemy
{

    private GameController s_gameController;

    private AudioManager s_audioManager = null;


    protected override void Start()
    {
        base.Start();

        s_gameController = GameController.Instance;
        s_audioManager = AudioManager.Instance;
    }


    protected override void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime);

        if (transform.position.x < -1 * s_gameController.Boundary_H)
            transform.position = new Vector3(s_gameController.Boundary_H - 0.5f, transform.position.y, 0);
        else if (transform.position.x > s_gameController.Boundary_H)
            transform.position = new Vector3(-1 * s_gameController.Boundary_H + 0.5f, transform.position.y, 0);
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Player player = i_collider.GetComponent<Player>();

        if (player != null)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);

            Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Shotgun : Bonus_Base
{

    private GameController s_gameController;

    private AudioManager s_audioManager = null;



    protected override void Init()
    {
        s_gameController = GameController.Instance;
        s_audioManager = AudioManager.Instance;
    }


    protected override void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime);

        if (transform.position.x < s_gameController.DownLeft.x)
            transform.position = new Vector3(s_gameController.TopRight.x - 0.5f, transform.position.y, 0);
        else if (transform.position.x > s_gameController.TopRight.x)
            transform.position = new Vector3(s_gameController.DownLeft.x + 0.5f, transform.position.y, 0);
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

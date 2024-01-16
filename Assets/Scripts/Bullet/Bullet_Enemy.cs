using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : Bullet
{

    [SerializeField] private List<AudioClip> m_audioClipList = new List<AudioClip>();

    private AudioManager s_audioManager = null;


    protected override void Start()
    {
        base.Start();

        s_audioManager = AudioManager.Instance;
    }


    protected override void CleanUp()
    {
        if (transform.position.y < m_cleanUpThreshold)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Player player = i_collider.GetComponent<Player>();
        Bullet_Player bullet = i_collider.GetComponent<Bullet_Player>();
        Bullet_Laser laser = i_collider.GetComponent<Bullet_Laser>();

        if (player != null || bullet != null)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);

            Destroy(gameObject);
        }
        else if (laser != null)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);
            Destroy(gameObject);
        }
        else
            return;
    }
}

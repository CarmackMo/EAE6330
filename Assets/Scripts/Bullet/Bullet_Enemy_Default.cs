using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet_Enemy_Default : Bullet
{
    [SerializeField] private float m_maxVel = 0.0f;
    [SerializeField] private float m_maxForce = 0.0f;
    [SerializeField] private float m_liveTime = 15.0f;

    [SerializeField] private List<AudioClip> m_audioClipList = new List<AudioClip>();

    private float m_bornTime = 0.0f;

    protected Vector2 m_pos = Vector2.zero;
    protected Vector2 m_acc = Vector2.zero;

    protected Player s_player = null;

    private AudioManager s_audioManager = null;


    protected override void Start()
    {
        base.Start();

        m_bornTime = Time.time;
        s_player = Player.Instance;
        s_audioManager = AudioManager.Instance;
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Player player = i_collider.GetComponent<Player>();
        Bullet_Player_Default bullet = i_collider.GetComponent<Bullet_Player_Default>();
        Bullet_Player_Laser laser = i_collider.GetComponent<Bullet_Player_Laser>();

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


    protected override void Movement()
    {
        FollowPlayer();
    }


    protected override void CleanUp()
    {
        base.CleanUp();

        if (Time.time -  m_bornTime > m_liveTime)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);
            Destroy(gameObject);
        }
    }


    private Vector2 DynamicSeek(Vector2 i_target)
    {
        // Calculate the desired force direction that points to the target
        Vector2 force = (i_target - m_pos).normalized;
        // Calculate the force magnitude
        force *= m_maxVel;
        // Constrain the force magnitude
        force = Vector2.ClampMagnitude(force, m_maxForce);

        return force;
    }


    private void FollowPlayer()
    {
        m_pos = transform.position;

        Vector2 force = DynamicSeek(s_player.transform.position);

        m_acc += (force / 1);

        m_vel += m_acc * Time.deltaTime;
        m_vel = Vector2.ClampMagnitude(m_vel, m_maxVel);
        m_pos += m_vel * Time.deltaTime;
        m_acc = Vector2.zero;

        transform.position = m_pos;
    }


}

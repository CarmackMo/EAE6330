using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Alien : Enemy_Base
{

    [SerializeField] Bullet_Enemy m_bullet_enemy = null;

    float m_lastShootTime = 0.0f;
    float m_shootCooldown = 0.0f;
    [SerializeField] Vector2 m_shootCooldownRange = Vector2.zero;

    private GameController s_gameController = null;
    private AudioManager s_audioManager = null;
    private Player s_player = null;



    protected override void Update()
    {
        base.Update();

        ShootBullet();   
    }


    protected override void Init()
    {
        s_gameController = GameController.Instance;
        s_audioManager = AudioManager.Instance;
        s_player = Player.Instance;

        m_shootCooldown = Random.Range(m_shootCooldownRange.x, m_shootCooldownRange.y);
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Rock rock = i_collider.GetComponent<Enemy_Rock>();
        Player player = i_collider.GetComponent<Player>();
        Bullet_Player bullet = i_collider.GetComponent<Bullet_Player>();
        Bullet_Laser laser = i_collider.GetComponent<Bullet_Laser>();

        if (rock != null)
        {
            m_speed.x *= -1;
        }
        else if (bullet != null) 
        {
            m_HP -= 1;
            if (m_HP == 0)
            {
                s_audioManager.PlayOneShot(m_audioClipList[0]);

                Destroy(gameObject);
            }
        }
        else if (player != null || laser != null)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);
            Destroy(gameObject);
        }
    }


    protected override void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime);

        if (transform.position.x > s_gameController.TopRight.x ||
            transform.position.x < s_gameController.DownLeft.x )
            m_speed.x *= -1;
    }


    private void ShootBullet()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastShootTime >= m_shootCooldown)
        {
            Vector3 playerPos = s_player.transform.position;
            Vector3 selfPos = transform.position;

            Vector3 velocity = (playerPos - selfPos).normalized * 1.5f;

            Bullet_Enemy newBullet = Instantiate(m_bullet_enemy, selfPos, transform.rotation);
            newBullet.Speed = velocity;


            m_lastShootTime = currentTime;
            m_shootCooldown = Random.Range(m_shootCooldownRange.x, m_shootCooldownRange.y);
        }
    }

}

using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class Enemy_Alien : Enemy_Base
{
    [SerializeField] private float m_radius_evade = 0.0f;
    [SerializeField] Bullet_Enemy_Default m_bullet_enemy = null;

    private Vector2 m_vel = Vector2.zero;
    private Vector2 m_pos = Vector2.zero;
    private Vector2 m_acc = Vector2.zero;

    private SM_Alien m_SM_patrol = null;
    private SM_Alien m_SM_combat = null;

    private GameController s_gameController = null;
    private AudioManager s_audioManager = null;
    private Player s_player = null;


    protected override void Update()
    {
        base.Update();

        m_SM_patrol.Update();   
        m_SM_combat.Update();
    }


    protected override void Init()
    {
        base.Init();

        // Init member variable
        {
            s_gameController = GameController.Instance;
            s_audioManager = AudioManager.Instance;
            s_player = Player.Instance;
        }

        // Init combat state machine
        {
            m_SM_combat = new SM_Alien();

            var cmd_idle = new Cmd_ChangeState<SM_Alien>(m_SM_combat, r => r.ChangeState_ToIdle());
            var cmd_shootBullet = new Cmd_ChangeState<SM_Alien>(m_SM_combat, r => r.ChangeState_ToShootBullet());

            State_Aline_Idle state_idle = new State_Aline_Idle(this, cmd_shootBullet);
            State_Aline_ShootBullet state_shootBullet = new State_Aline_ShootBullet(this, cmd_idle);

            Dictionary<string, IState> states = new Dictionary<string, IState>
            {
                {nameof(State_Aline_Idle), state_idle },
                {nameof(State_Aline_ShootBullet), state_shootBullet },

            };

            m_SM_combat.Init(states, nameof(State_Aline_Idle));
        }

        // Init patrol state machine
        {
            m_SM_patrol = new SM_Alien();

            State_Aline_Patrol state_patrol = new State_Aline_Patrol(this);

            Dictionary<string, IState> states = new Dictionary<string, IState>
            {
                { nameof(State_Aline_Patrol), state_patrol },
            };

            m_SM_patrol.Init(states, nameof(State_Aline_Patrol));

        }
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Rock rock = i_collider.GetComponent<Enemy_Rock>();
        Player player = i_collider.GetComponent<Player>();
        Bullet_Player_Default bullet = i_collider.GetComponent<Bullet_Player_Default>();
        Bullet_Player_Laser laser = i_collider.GetComponent<Bullet_Player_Laser>();

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
        m_pos = transform.position;

        m_pos += m_vel * Time.deltaTime;
        m_vel += (m_speed + m_acc) * Time.deltaTime;

        m_acc = Vector2.zero;

        transform.position = m_pos;


        if (transform.position.x > s_gameController.TopRight.x ||
            transform.position.x < s_gameController.DownLeft.x )
        {
            m_vel.x *= -1;
            m_speed.x *= -1;
        }
    }


    // Interface 


    public void ShootBullet()
    {
        Vector3 playerPos = s_player.transform.position;
        Vector3 selfPos = transform.position;

        Vector3 velocity = (playerPos - selfPos).normalized * 1.5f;

        Bullet_Enemy_Default newBullet = Instantiate(m_bullet_enemy, selfPos, transform.rotation);
        //newBullet.Velocity = velocity;
    }


    public void EvadeObstacle()
    {
        int rockMate = 0;
        Vector2 steering = Vector2.zero;
        var rockList = EnemyGenerator.Instance.RockList;

        if (Vector2.Distance(s_player.transform.position, m_pos) <= m_radius_evade)
        {
            steering = -1 * (s_player.transform.position - transform.position);
        }

        foreach (Enemy_Rock rock in rockList)
        {
            if (rock != this && Vector2.Distance(m_pos, rock.transform.position) <= m_radius_evade)
            {
                Vector2 force = (transform.position - rock.transform.position);
                // steer = steer / d
                force = force * (0.25f / Vector2.Distance(m_pos, rock.transform.position));
                steering += force;
                rockMate++;
            }
        }

        steering.Normalize();
        steering *= 10.0f;
        steering = Vector2.ClampMagnitude(steering, 25.0f);

        m_acc += steering;
    }
}

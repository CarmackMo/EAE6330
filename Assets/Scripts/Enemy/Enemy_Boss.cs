using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy_Base
{
    [SerializeField] private float m_raidus_defend = 0.0f;
    [SerializeField] private float m_radius_evade = 0.0f;

    [SerializeField] private GameObject m_shield = null;
    [SerializeField] private Enemy_Rock m_rock = null;
    [SerializeField] private Bullet_Enemy_Boid m_boid = null;

    private Vector2 m_vel = Vector2.zero;
    private Vector2 m_pos = Vector2.zero;
    private Vector2 m_acc = Vector2.zero;

    private Player s_player = null;

    private SM_Boss m_SM_combat = null;
    private SM_Boss m_SM_patrol = null;

    private List<Bullet_Enemy_Boid> m_boidList = new List<Bullet_Enemy_Boid>(); 



    protected override void Start()
    {
        base.Start();
    }


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
            s_player = Player.Instance;
        }

        // Init combat state machine
        {
            m_SM_combat = new SM_Boss();

            var cmd_defend = new Cmd_ChangeState<SM_Boss>(m_SM_combat, r => r.ChangeState_ToDefend());
            var cmd_idle = new Cmd_ChangeState<SM_Boss>(m_SM_combat, r => r.ChangeState_ToIdel());
            var cmd_boid = new Cmd_ChangeState<SM_Boss>(m_SM_combat, r => r.ChangeState_ToBoid());
            var cmd_shootRock = new Cmd_ChangeState<SM_Boss>(m_SM_combat, r => r.ChangeState_ToShootRock());

            State_Boss_Idle state_idle = new State_Boss_Idle(this, cmd_defend, cmd_boid, cmd_shootRock);
            State_Boss_Defend state_defend = new State_Boss_Defend(this, cmd_idle);
            State_Boss_Boid state_boid = new State_Boss_Boid(this, cmd_idle);
            State_Boss_ShootRock state_shootRock = new State_Boss_ShootRock(this, cmd_idle);

            Dictionary<string, IState> states = new Dictionary<string, IState>
            {
                {nameof(State_Boss_Idle), state_idle },
                {nameof(State_Boss_Defend), state_defend },
                {nameof(State_Boss_Boid), state_boid },
                {nameof(State_Boss_ShootRock), state_shootRock },
            };

            m_SM_combat.Init(states, nameof(State_Boss_Idle));
        }

        // Init patrol state machine
        {
            m_SM_patrol = new SM_Boss();

            State_Boss_Patrol state_patrol = new State_Boss_Patrol(this);

            Dictionary<string, IState> states = new Dictionary<string, IState>
            {
                { nameof(State_Boss_Patrol), state_patrol },
            };

            m_SM_patrol.Init(states, nameof(State_Boss_Patrol));
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
                //s_audioManager.PlayOneShot(m_audioClipList[0]);
                EnemyGenerator.Instance.DeregisterBoss();
                Destroy(gameObject);
            }
        }
        else if (player != null || laser != null)
        {
            //s_audioManager.PlayOneShot(m_audioClipList[0]);
            EnemyGenerator.Instance.DeregisterBoss();
            Destroy(gameObject);
        }
    }


    protected override void Movement()
    {
        m_pos = transform.position;

        m_pos += m_vel * Time.deltaTime;
        m_vel += m_acc * Time.deltaTime;

        m_acc = Vector2.zero;

        transform.position = m_pos;
    }


    // Interface

    public bool DetectBullet()
    {
        foreach (Bullet bullet in s_player.PlayerBullets)
        {
            if (bullet == null) 
                continue;
            if (Vector2.Distance(transform.position, bullet.transform.position) <= m_raidus_defend)
                return true;
        }

        return false;
    }


    public void EnableDefence(bool i_active)
    {
        m_shield.SetActive(i_active);
    }


    public void ShootRock()
    {
        Player player = Player.Instance;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 rockSpeed = direction * 2.5f;
       
        Enemy_Rock newRock = Instantiate(m_rock, transform.position, transform.rotation);
        newRock.Speed = rockSpeed;
    }


    public void ShootBoid()
    {
        bool isLead;
        Vector2 speed;
        Bullet_Enemy_Boid newBoid;

        isLead = Random.Range(0.0f, 100.0f) <= 20 ? true : false; 
        speed = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
        newBoid = Instantiate(m_boid, transform.position, transform.rotation);
        newBoid.Init(transform.position, speed, isLead, m_boidList);
        m_boidList.Add(newBoid);

        isLead = Random.Range(0.0f, 100.0f) <= 20 ? true : false;
        speed = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
        newBoid = Instantiate(m_boid, transform.position, transform.rotation);
        newBoid.Init(transform.position, speed, isLead, m_boidList);
        m_boidList.Add(newBoid);

        isLead = Random.Range(0.0f, 100.0f) <= 20 ? true : false;
        speed = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
        newBoid = Instantiate(m_boid, transform.position, transform.rotation);
        newBoid.Init(transform.position, speed, isLead, m_boidList);
        m_boidList.Add(newBoid);
    }


    public void EvadeRock()
    {
        int rockMate = 0;
        Vector2 steering = Vector2.zero;
        var rockList = EnemyGenerator.Instance.RockList;

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

        if (Mathf.Abs(m_pos.x - m_downLeft.x) < m_radius_evade)
            steering += Vector2.right * 30.0f;
        else if (Mathf.Abs(m_pos.x - m_topRight.x) < m_radius_evade)
            steering += Vector2.left * 30.0f;

        if (Mathf.Abs(m_pos.y - m_downLeft.y) < m_radius_evade)
            steering += Vector2.up * 30.0f;
        else if (Mathf.Abs(m_pos.y - m_topRight.y) < m_radius_evade)
            steering += Vector2.down * 30.0f;

        steering.Normalize();
        steering *= 17.0f;
        steering -= m_vel;
        steering = Vector2.ClampMagnitude(steering, 25.0f);

        m_acc += steering;
    }

    public void UpdateVel()
    {
        m_vel += new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));
    }


}

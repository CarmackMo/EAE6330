using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy_Base
{

    [SerializeField] private float m_defnedRaidus = 0.0f;

    [SerializeField] private GameObject m_shield = null;

    [SerializeField] private Enemy_Rock m_rock = null;
    

    private Player s_player = null;

    private SM_Boss m_stateMachine = null;

    private List<Bullet_Enemy_Boid> m_boidList = new List<Bullet_Enemy_Boid>(); 



    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();


        m_stateMachine.Update();
    }


    protected override void Init()
    {
        base.Init();

        // Init member variable
        {
            s_player = Player.Instance;
        }

        // Init state machine
        {
            m_stateMachine = new SM_Boss();

            var cmd_defend = new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToDefend());
            var cmd_idle = new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToIdel());
            var cmd_boid = new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToBoid());
            var cmd_shootRock = new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToShootRock());

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

            m_stateMachine.Init(states, nameof(State_Boss_Idle));
        }

    }


    protected override void OnTriggerEnter2D(Collider2D i_collider) { }


    protected override void Movement() { }


    // Interface

    public bool DetectBullet()
    {
        foreach (Bullet bullet in s_player.PlayerBullets)
        {
            if (bullet == null) 
                continue;
            if (Vector2.Distance(transform.position, bullet.transform.position) <= m_defnedRaidus)
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


}

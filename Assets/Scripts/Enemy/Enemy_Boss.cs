using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy_Base
{

    [SerializeField] private float m_defnedRaidus = 0.0f;

    [SerializeField] private GameObject m_shield = null;

    

    private Player s_player = null;

    private SM_Boss m_stateMachine = null;




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

            State_Boss_Idle state_idle = new State_Boss_Idle(this, cmd_defend, cmd_boid);
            State_Boss_Defend state_defend = new State_Boss_Defend(this, cmd_idle);
            State_Boss_Boid state_boid = new State_Boss_Boid(this, cmd_idle);

            Dictionary<string, IState> states = new Dictionary<string, IState>
            {
                {nameof(State_Boss_Idle), state_idle },
                {nameof(State_Boss_Defend), state_defend },
                {nameof(State_Boss_Boid), state_boid },
            };

            m_stateMachine.Init(states, nameof(State_Boss_Idle));
        }

    }


    protected override void OnTriggerEnter2D(Collider2D i_collider) { }


    protected override void Movement() { }


    private void ShootBullet() { }


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


}

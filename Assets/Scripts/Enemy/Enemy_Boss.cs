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
        s_player = Player.Instance;

        m_stateMachine = new SM_Boss();

        State_Boss_Idle state_idle = new State_Boss_Idle(this, new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToDefend()));
        State_Boss_Defend state_defend = new State_Boss_Defend(this, new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToIdel()));

        Dictionary<string, IState> states = new Dictionary<string, IState>
        {
            {nameof(State_Boss_Idle), state_idle },
            {nameof(State_Boss_Defend), state_defend },
        };

        m_stateMachine.Init(states, nameof(State_Boss_Idle));
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

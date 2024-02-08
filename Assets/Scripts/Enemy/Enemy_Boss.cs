using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Boss : Enemy_Base
{

    SM_Boss m_stateMachine = null;


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
        m_stateMachine = new SM_Boss();
    
        Dictionary<string, IState> states = new Dictionary<string, IState>
        {
            {nameof(State_Boss_Idle), new State_Boss_Idle(new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToDefend())) },
            {nameof(State_Boss_Defend), new State_Boss_Defend(new Cmd_ChangeState<SM_Boss>(m_stateMachine, r => r.ChangeState_ToIdel())) },
        };

        m_stateMachine.Init(states, nameof(State_Boss_Idle));
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {

    }


    protected override void Movement()
    {

    }


    private void ShootBullet()
    {

    }




}

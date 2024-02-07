using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
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
        Dictionary<string, IState> states = new Dictionary<string, IState>
        {
            {nameof(State_Boss_I), new State_Boss_I() },
            {nameof(State_Boss_II), new State_Boss_II() },
        };

        m_stateMachine = new SM_Boss();
        m_stateMachine.Init(states, nameof(State_Boss_I));
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

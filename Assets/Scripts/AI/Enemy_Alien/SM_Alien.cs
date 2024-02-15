using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Alien : IStateMachine
{
    public void ChangeState_ToIdle()
    {
        ChangeState(m_states[nameof(State_Aline_Idle)]);
    }


    public void ChangeState_ToShootBullet() 
    {
        ChangeState(m_states[nameof(State_Aline_ShootBullet)]);
    }
}

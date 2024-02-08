using System.Collections.Generic;
using UnityEngine;

public class SM_Boss : IStateMachine
{



    public void ChangeState_ToIdel()
    {
        ChangeState(m_states[nameof(State_Boss_Idle)]);
    }


    public void ChangeState_ToDefend()
    {
        ChangeState(m_states[nameof(State_Boss_Defend)]);
    }

}
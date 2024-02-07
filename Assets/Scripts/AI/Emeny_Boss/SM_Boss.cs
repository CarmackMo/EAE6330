using System.Collections.Generic;
using UnityEngine;

public class SM_Boss : IStateMachine
{
    private float m_lastUpdateTime = 0.0f;





    public override void Update()
    {
        base.Update();

        if (Time.time - m_lastUpdateTime > 4)
        {

            if (m_currState == m_states[nameof(State_Boss_I)])
                ChangeState(m_states[nameof(State_Boss_II)]);
            else
                ChangeState(m_states[nameof(State_Boss_I)]);

            m_lastUpdateTime = Time.time;
        }
    }
}

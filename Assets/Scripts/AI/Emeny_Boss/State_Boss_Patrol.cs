using UnityEngine;


public class State_Boss_Patrol : IState
{

    private float m_time_lastUpdate = 0.0f;
    private float m_time_updateCoolDown = 4.5f;

    private Enemy_Boss m_Boss = null;

    public State_Boss_Patrol(Enemy_Boss i_boss)
    {
        m_Boss = i_boss;
    }


    public void Init()
    {

    }


    public void Update()
    {
        m_Boss.EvadeRock();

        if (Time.time - m_time_lastUpdate > m_time_updateCoolDown)
        {
            m_Boss.UpdateVel();
            m_time_lastUpdate = Time.time;  
        }
    }


    public void Enter()
    {
        Debug.Log("Boss_Patrol: Enter");
    }


    public void Exit()
    {
        Debug.Log("Boss_Patrol: Exit");
    }

}

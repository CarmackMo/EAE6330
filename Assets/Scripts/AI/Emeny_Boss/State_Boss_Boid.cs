using UnityEngine;


public class State_Boss_Boid : IState
{

    private float m_time_onEnter = 0.0f;
    private float m_time_duration = 0.0f;

    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_idle = null;

    private Enemy_Boss m_Boss = null;



    public State_Boss_Boid( Enemy_Boss i_boss, Cmd_ChangeState<SM_Boss> i_cmd_idle)
    {
        m_time_duration = 5.0f;

        m_Boss = i_boss;
        m_cmd_changeState_idle = i_cmd_idle;
    }


    public void Init() 
    {

    }


    public void Update() 
    {
        float currTime = Time.time;
        if (currTime - m_time_onEnter >= m_time_duration)
        {
            m_cmd_changeState_idle.Execute();
        }
    }


    public void Enter()
    {
        Debug.Log("Boss_Boid: Enter");

        m_time_onEnter = Time.time;
    }


    public void Exit()
    {
        Debug.Log("Boss_Boid: E:xit");
    }

}

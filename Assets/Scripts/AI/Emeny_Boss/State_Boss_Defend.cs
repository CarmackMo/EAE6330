using UnityEngine;


public class State_Boss_Defend : IState
{

    private float m_time_onEnter = 0.0f;
    private float m_time_duration = 0.0f;

    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_idle = null;

    private Enemy_Boss m_Boss = null;


    public State_Boss_Defend(Enemy_Boss i_boss, Cmd_ChangeState<SM_Boss> i_cmd)
    {
        m_time_duration = 5.0f;

        m_Boss = i_boss;
        m_cmd_changeState_idle = i_cmd;
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
        Debug.Log("Boss_Defend: Enter");

        m_time_onEnter = Time.time;
        m_Boss.EnableDefence(true);
    }


    public void Exit()
    {
        Debug.Log("Boss_Defend: Exit");

        m_Boss.EnableDefence(false);
    }

}

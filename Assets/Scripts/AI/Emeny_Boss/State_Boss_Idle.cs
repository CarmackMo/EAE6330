using UnityEngine;


public class State_Boss_Idle : IState
{

    private float temp_time_enter = 0.0f;

    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_defend = null;


    public State_Boss_Idle(Cmd_ChangeState<SM_Boss> i_cmd)
    {
        m_cmd_changeState_defend = i_cmd;
    }


    public void Init() 
    {

    }


    public void Update() 
    {
        if (Time.time - temp_time_enter > 3) 
        {
            m_cmd_changeState_defend.Execute();
        }

        Debug.Log("Boss_Idle: update");
    }


    public void Enter()
    {
        temp_time_enter = Time.time;
        Debug.Log("Boss_Idle: Enter");
    }


    public void Exit()
    {
        Debug.Log("Boss_Idle: Update");
    }

}

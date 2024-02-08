using UnityEngine;


public class State_Boss_Defend : IState
{

    private float temp_time_enter = 0.0f;

    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_idle = null;


    public State_Boss_Defend(Cmd_ChangeState<SM_Boss> i_cmd)
    {
        m_cmd_changeState_idle = i_cmd;
    }
    

    public void Init() 
    {

    }


    public void Update() 
    {
        if (Time.time - temp_time_enter > 3)
        {
            m_cmd_changeState_idle.Execute();
        }

        Debug.Log("Boss_Defend: update");
    }


    public void Enter()
    {
        temp_time_enter = Time.time;
        Debug.Log("Boss_Defend: Update");
    }


    public void Exit()
    {
        Debug.Log("Boss_Defend: Exit");
    }

}

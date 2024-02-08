using UnityEngine;


public class State_Boss_Defend : IState
{

    private float temp_time_enter = 0.0f;

    private Cmd_OnStateExit<SM_Boss> m_cmd_changeState_toIdle = null;


    public State_Boss_Defend(Cmd_OnStateExit<SM_Boss> i_cmd)
    {
        m_cmd_changeState_toIdle = i_cmd;
    }
    

    public void Init() 
    {

    }


    public void Update() 
    {
        if (Time.time - temp_time_enter > 3)
        {
            m_cmd_changeState_toIdle.Execute();
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

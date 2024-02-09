using UnityEngine;


public class State_Boss_Idle : IState
{

    private float m_time_lastDefend = 0.0f;
    private float m_time_defendCooldown = 0.0f;

    private float m_time_lastShoot = 0.0f;
    private float m_time_shootCoolDown = 0.0f;

    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_defend = null;
    private Cmd_ChangeState<SM_Boss> m_cmd_changeState_shootBoid = null;

    private Enemy_Boss m_Boss = null;



    public State_Boss_Idle(
        Enemy_Boss i_boss, 
        Cmd_ChangeState<SM_Boss> i_cmd_defend, 
        Cmd_ChangeState<SM_Boss> i_cmd_boid)
    {
        m_time_lastDefend = 0.0f;
        m_time_defendCooldown = 10.0f;
        m_time_lastShoot = 0.0f;
        m_time_shootCoolDown = 10.0f;

        m_Boss = i_boss;
        m_cmd_changeState_defend = i_cmd_defend;
        m_cmd_changeState_shootBoid = i_cmd_boid;
    }


    public void Init() 
    {

    }


    public void Update() 
    {
        float currTime = Time.time;
        if (m_Boss.DetectBullet() && currTime - m_time_lastDefend >= m_time_defendCooldown)
        {
            m_time_lastDefend = currTime;
            m_cmd_changeState_defend.Execute();
        }
        else if (currTime - m_time_lastShoot >= m_time_shootCoolDown)
        {
            m_time_lastShoot = currTime;
            m_cmd_changeState_shootBoid.Execute();
        }
    }


    public void Enter()
    {
        Debug.Log("Boss_Idle: Enter");
    }


    public void Exit()
    {
        Debug.Log("Boss_Idle: E:xit");
    }

}

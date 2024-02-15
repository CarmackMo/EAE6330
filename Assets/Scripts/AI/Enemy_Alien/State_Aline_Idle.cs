using UnityEngine;


public class State_Aline_Idle : IState
{
    private float m_time_lastShoot = 0.0f;
    private float m_time_shootCoolDown = 0.0f;

    private Cmd_ChangeState<SM_Alien> m_cmd_changeState_shootBullet = null;

    private Enemy_Alien m_alien = null;

    public State_Aline_Idle(
        Enemy_Alien i_alien,
        Cmd_ChangeState<SM_Alien> i_cmd_shootBullet)
    {
        m_alien = i_alien;
        m_time_lastShoot = -5.0f;
        m_time_shootCoolDown = 10.0f;

        m_cmd_changeState_shootBullet = i_cmd_shootBullet;
    }


    public void Init()
    {

    }


    public void Update()
    {
        float currTime = Time.time;
        if (currTime - m_time_lastShoot >= m_time_shootCoolDown)
        {
            m_time_lastShoot = currTime;
            m_cmd_changeState_shootBullet.Execute();
        }
    }


    public void Enter()
    {
        Debug.Log("Aline_Idle: Enter");
    }


    public void Exit()
    {
        Debug.Log("Aline_Idle: E:xit");
    }

}

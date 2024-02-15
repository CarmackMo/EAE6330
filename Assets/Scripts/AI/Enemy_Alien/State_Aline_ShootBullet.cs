using UnityEngine;


public class State_Aline_ShootBullet : IState
{
    private Cmd_ChangeState<SM_Alien> m_cmd_changeState_idle = null;

    private Enemy_Alien m_alien = null;


    public State_Aline_ShootBullet(Enemy_Alien i_alien, Cmd_ChangeState<SM_Alien> i_cmd_idle)
    {
        m_alien = i_alien;
        m_cmd_changeState_idle = i_cmd_idle;
    }


    public void Init()
    {
    }


    public void Update()
    {
    }


    public void Enter()
    {
        Debug.Log("Alien_ShootBullet: Enter");

        m_alien.ShootBullet();

        m_cmd_changeState_idle.Execute();
    }


    public void Exit()
    {
        Debug.Log("Alien_ShootBullet: Exit");
    }

}

using UnityEngine;


public class State_Aline_Patrol : IState
{
    private float m_time_lastUpdate = 0.0f;
    private float m_time_updateCoolDown = 4.5f;

    private Enemy_Alien m_alien = null;

    public State_Aline_Patrol(Enemy_Alien i_alien)
    {
        m_alien = i_alien;
    }


    public void Init()
    {

    }


    public void Update()
    {
        m_alien.EvadeObstacle();
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

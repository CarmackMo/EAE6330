using UnityEngine;


public class State_Boss_II : IState
{

    public void Init() 
    {
        throw new System.NotImplementedException();
    }

    public void Update() 
    {
        Debug.Log("Boss_2: update");
    }

    public void Enter()
    {
        Debug.Log("Boss_2: enter");
    }

    public void Exit()
    {
        Debug.Log("Boss_2: exit");
    }




}

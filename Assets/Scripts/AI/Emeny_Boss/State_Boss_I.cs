using UnityEngine;


public class State_Boss_I : IState
{

    public void Init() 
    {
        throw new System.NotImplementedException();
    }

    public void Update() 
    {
        Debug.Log("Boss_1: update");
    }

    public void Enter()
    {
        Debug.Log("Boss_1: enter");
    }

    public void Exit()
    {
        Debug.Log("Boss_1: exit");
    }




}

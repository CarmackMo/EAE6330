
using System;


public class Cmd_ChangeState<TReciver> : Command_Base<TReciver> where TReciver : IStateMachine
{
    public Cmd_ChangeState(TReciver i_receiver, Action<TReciver> i_action)
        : base(i_receiver, i_action)
    { }
}


public interface IState
{

    public void Init();

    public void Enter();

    public void Update();

    public void Exit();

}

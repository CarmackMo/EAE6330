using System;


public interface ICommand
{
    public void Execute();
}


public class Command_Base<TReceiver> : ICommand where TReceiver : class
{

    protected TReceiver m_receiver = null;

    protected Action<TReceiver> m_action = null;


    public Command_Base(TReceiver i_receiver, Action<TReceiver> i_action)
    {
        m_receiver = i_receiver;
        m_action = i_action;
    }


    public TReceiver Receiver()
    {
        return m_receiver;
    }


    public virtual void Execute()
    {
        m_action.Invoke(m_receiver);
    }
}
using System;


public interface ICommand
{
    public void Execute();
}


class Command<TReceiver> : ICommand where TReceiver : class
{
    private TReceiver m_receiver = null;
    public TReceiver Receiver { get { return m_receiver; } }

    private Action<TReceiver> m_action = null;


    public Command(TReceiver i_receiver, Action<TReceiver> i_action)
    {
        m_receiver = i_receiver;
        m_action = i_action;
    }


    public void Execute()
    {
        m_action.Invoke(m_receiver);
    }
}
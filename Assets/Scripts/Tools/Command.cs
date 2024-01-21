using System;


public interface ICommand
{
    public void Execute();
}


public enum ECmdType { None, RightMouse, ScrollMouse };

class Command<TReceiver> : ICommand where TReceiver : class
{
    private TReceiver m_receiver = null;
    public TReceiver receiver { get { return m_receiver; } }

    private Action<TReceiver> m_action = null;
    public Action<TReceiver> action { get { return m_action; } }

    private ECmdType m_cmdType = ECmdType.None;
    public ECmdType CmdType {  get { return m_cmdType; } }


    public Command(TReceiver i_receiver, Action<TReceiver> i_action, ECmdType i_type)
    {
        m_receiver = i_receiver;
        m_action = i_action;
        m_cmdType = i_type;
    }


    public void Execute()
    {
        m_action.Invoke(m_receiver);
    }
}
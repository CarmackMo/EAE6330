using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public void Execute();
}



class Command<TReceiver> : ICommand where TReceiver : class
{
    private TReceiver m_receiver = null;
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
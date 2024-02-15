
using System.Collections.Generic;
using Unity.VisualScripting;

public class IStateMachine
{

    protected IState m_currState = null;
    protected Dictionary<string, IState> m_states = new Dictionary<string, IState>();
    

    public IState CurrState { get { return m_currState; } }
    public Dictionary<string, IState> States { get { return m_states; } }



    public virtual void Init(Dictionary<string, IState> i_states, string i_initState)
    {
        m_states = i_states;

        ChangeState(m_states[i_initState]);
    }


    public virtual void ChangeState(IState i_newState)
    {
        m_currState?.Exit();

        m_currState = i_newState;

        m_currState?.Enter();
    }


    public virtual void Update()
    {
        m_currState?.Update();
    }


    public virtual void InitCurrState()
    {
        m_currState?.Init();
    }


}

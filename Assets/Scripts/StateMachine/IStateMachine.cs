
public class IStateMachine
{

    protected IState m_currState = null;

    public IState CurrState { get { return m_currState; } }



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

using UnityEngine;



public class GameInitializer : Singleton<GameInitializer>
{
    // Data
    //=========================

    [SerializeField] private int m_totalProperty = 0;

    private int m_wealth = 1;
    private int m_strength = 1;
    private int m_IQ = 1;

    private StartPanel s_startPanel = null;
    private GameplayPanel s_gameplayPanel = null;
    private EventGenerator s_eventGenerator = null;



    // Implementation
    //=========================

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        s_eventGenerator = EventGenerator.Instance;
        s_startPanel = StartPanel.Instance;
        s_gameplayPanel = GameplayPanel.Instance;
    }



    // Interface
    //=========================

    public int TotalProperty { get { return m_totalProperty; }  private set { } }
    public int Wealth { get { return m_wealth; } private set { } }
    public int Strength { get { return m_strength; } private set { } }
    public int IQ { get { return m_IQ; } private set { } }


    public void StartGame()
    {
        s_eventGenerator.Wealth = m_wealth;
        s_eventGenerator.Strength = m_strength;
        s_eventGenerator.IQ = m_IQ;

        s_startPanel.SetVisible(false);
        s_gameplayPanel.UpdateUI();
        s_gameplayPanel.SetVisible(true);
        s_eventGenerator.SetActive(true);
        s_eventGenerator.StartGame();
    }


    public void AddWealth()
    {
        if (m_totalProperty > 0)
        {
            m_wealth++;
            m_totalProperty--;
        }
    }


    public void SubWealth()
    {
        if (m_wealth > 0)
        {
            m_wealth--;
            m_totalProperty++; 
        }
    }


    public void AddStrength()
    {
        if (m_totalProperty > 0)
        {
            m_strength++;
            m_totalProperty--;
        }
    }


    public void SubStrength()
    {
        if (m_strength > 0)
        {
            m_strength--;
            m_totalProperty++;
        }
    }


    public void AddIQ()
    {
        if (m_totalProperty > 0) 
        {
            m_IQ++;
            m_totalProperty--;
        }
    }


    public void SubIQ()
    {
        if (m_IQ > 0)
        {
            m_IQ--;
            m_totalProperty++;
        }
    }

}

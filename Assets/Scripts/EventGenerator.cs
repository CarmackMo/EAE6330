using UnityEngine;


public class EventGenerator : Singleton<EventGenerator>
{
    // Data
    //=========================

    [SerializeField] private float m_rate_normalEvent = 0.0f;
    [SerializeField] private float m_rate_wealthEvent = 0.0f;
    [SerializeField] private float m_rate_deathByWeak = 0.0f;
    [SerializeField] private float m_rate_deathByOld = 0.0f;
    [SerializeField] private float m_rate_deathAtBegin = 0.0f;
    [SerializeField] private float m_eventCoolDownTime = 0.0f;
    [SerializeField] private int m_propertyPoint = 0;

    [SerializeField] private NormalEventAsset m_normalEvent = null;
    [SerializeField] private EventAsset m_wealthEvent = null;
    [SerializeField] private EventAsset m_strengthEvent = null;

    private int m_wealth = 0;
    private int m_strength = 0;
    private int m_IQ = 0;
    private int m_age = 0;

    private int m_tmpProperty = 0;
    private int m_tmpWealht = 0;
    private int m_tmpStrength = 0;

    private bool m_active = false;

    private float m_currentTime = 0.0f;
    private float m_lastEventTime = 0.0f;

    private GameplayPanel s_gameplayPanel = null;


    // Implementation
    //=========================

    void Start()
    {
        Init();
    }


    void Update()
    {
        GameLoop();

        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }


    private void Init()
    {
        s_gameplayPanel = GameplayPanel.Instance;

        SetActive(false);
    }


    private void GameLoop()
    {
        if (m_currentTime - m_lastEventTime >= m_eventCoolDownTime)
        {
            CheckEndCondition();
            GenerateEvent();
            CheckSettingCondition();

            m_lastEventTime = m_currentTime;
        }


        if (m_active)
        {
            m_currentTime += Time.deltaTime;
        }
    }


    private void GenerateEvent()
    {
        if (!m_active)
            return;

        int pivot = 0;
        string playerEvent = m_age.ToString() + " years old. ";
        EventResult eventRes;
        pivot = UnityEngine.Random.Range(0, 100);


        if (pivot <= m_rate_normalEvent)
        {
            playerEvent += m_normalEvent.GetEvent();
        }
        else
        {
            pivot = UnityEngine.Random.Range(0, 100);
            if (pivot <= m_rate_wealthEvent)
            {
                pivot = UnityEngine.Random.Range(0, 10);
                bool status = (pivot <= m_wealth) ? true : false;

                eventRes = m_wealthEvent.GetEvent(status);
                playerEvent += eventRes.text;
            }
            else
            {
                pivot = UnityEngine.Random.Range(0, 10);
                bool status = (pivot <= m_strength) ? true : false;

                eventRes = m_wealthEvent.GetEvent(status);
                playerEvent += eventRes.text;
            }


            string delta = "";
            if (eventRes.deltaWealth != 0)
            {
                delta += " You get " + eventRes.deltaWealth.ToString() + " wealth.";
                m_wealth += eventRes.deltaWealth;
                s_gameplayPanel.UpdateUI();
            }
            if (eventRes.deltaStrength != 0)
            {
                delta += " You get " + eventRes.deltaStrength.ToString() + " strength.";
                m_strength += eventRes.deltaStrength;
                s_gameplayPanel.UpdateUI();
            }

            playerEvent += delta;

        }

        s_gameplayPanel.AddEvent(playerEvent);
        m_age++;
    }


    private void CheckSettingCondition()
    {
        if (!m_active)
            return;

        if (m_age == 3 || m_age == 30 || 
            m_age == 45 || m_age == 60 || 
            m_age == 75 || m_age == 90)
        {
            SetActive(false);
            
            m_tmpProperty = m_propertyPoint;
            m_tmpStrength = 0;
            m_tmpWealht = 0;

            s_gameplayPanel.UpdateUI();
            s_gameplayPanel.SetMenuVisible(true);
        }
    }


    private void CheckEndCondition()
    {
        if (!m_active)
            return;

        bool res = true;
        string text = "";
        float pivot = Random.Range(0.0f, 100.0f);

        if (m_age == 0 && pivot <= m_rate_deathAtBegin)
        {
            text = "Something went wrong during your birth and you died. But at least you can restart right away.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }

        if (m_strength <= -10)
        {
            text = "You die. F**K this world!";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }
        else if (m_strength <= 2 && pivot <= m_rate_deathByWeak)
        {
            text = "You have a serious sick, and you were not strong enough to make it, you die.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }
        else if (m_strength <= 0 && pivot <= m_rate_deathByWeak + 55)
        {
            text = "You are too week, Darwin thought you were slowing down human evolution, so you died.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }
        // Death by old
        else if (m_age >= 65 && pivot <= m_rate_deathByOld)
        {
            text = "You lived a peaceful life and eventually you left this world in dreamland. You died.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }
        else if (m_age >= 80 && pivot <= m_rate_deathByOld + 20)
        {
            text = "God decided you'd lived long enough, or maybe God just wanted you to be there for him, so you died.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }
        else if (m_age >= 95 && pivot <= m_rate_deathByOld + 50)
        {
            text = "You broke a Guinness World Record for longevity, but monopolizing the record list is clearly selfish. So you decided to die, leaving the chance to break the record to someone else in the future.";
            s_gameplayPanel.AddEvent(text);
            res = false;
        }

        SetActive(res);
    }



    // Interface
    //=========================

    public int Wealth { get { return m_wealth; } set { m_wealth = value; } }
    public int Strength { get { return m_strength; } set { m_strength = value; } }
    public int IQ { get { return m_IQ; } set { m_IQ = value; } }
    public int Property { get { return m_tmpProperty; }  private set { } }
    public int TmpWealth { get { return m_tmpWealht; } private set { } }
    public int TmpStrength { get { return m_tmpStrength; } private set { } }    


    public void SetActive(bool i_active)
    {
        m_active = i_active;
    }


    public void StartGame()
    {
        m_currentTime = Time.time;
        CheckEndCondition();

        if (m_active)
        {
            s_gameplayPanel.AddEvent("You are born");
            m_age++;
        }
    }


    public void AddTmpWealth()
    {
        if (m_tmpProperty > 0)
        {
            m_tmpWealht++;
            m_tmpProperty--;
        }
    }


    public void SubTmpWealth()
    {
        if (m_tmpWealht > 0)
        {
            m_tmpWealht--;
            m_tmpProperty++;
        }
    }


    public void AddTmpStrength()
    {
        if (m_tmpProperty > 0)
        {
            m_tmpStrength++;
            m_tmpProperty--;
        }
    }


    public void SubTmpStrength()
    {
        if (m_tmpStrength > 0)
        {
            m_tmpStrength--;
            m_tmpProperty++;
        }
    }


    public void ApplySetting()
    {
        m_wealth += m_tmpWealht;
        m_strength += m_tmpStrength;

        s_gameplayPanel.SetMenuVisible(false);

        SetActive(true);
    }
}

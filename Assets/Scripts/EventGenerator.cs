using System;
using UnityEngine;


public class EventGenerator : Singleton<EventGenerator>
{
    // Data
    //=========================

    [SerializeField] private float m_rate_normalEvent = 0.0f;
    [SerializeField] private float m_rate_wealthEvent = 0.0f;

    [SerializeField] private NormalEventAsset m_normalEvent = null;
    [SerializeField] private EventAsset m_wealthEvent = null;
    [SerializeField] private EventAsset m_strengthEvent = null;


    private int m_wealth = 0;
    private int m_strength = 0;
    private int m_IQ = 0;


    private float lastAdd = 0.0f;
    private float coolDown = 4.0f;

    private float m_currentTime = 0.0f;


    private GameplayPanel s_gameplayPanel = null;


    // Implementation
    //=========================

    void Start()
    {
        Init();
    }


    void Update()
    {
        
        if (m_currentTime - lastAdd >= coolDown) 
        {
            GenerateEvent();

            lastAdd = m_currentTime;    
        }

        m_currentTime += Time.deltaTime;
    }


    private void Init()
    {
        s_gameplayPanel = GameplayPanel.Instance;

        SetActive(false);
    }


    private void GenerateEvent()
    {
        int pivot = 0;
        string playerEvent = "";
        EventResult eventRes;
        pivot = UnityEngine.Random.Range(0, 100);

        if (pivot <= m_rate_normalEvent)
        {
            playerEvent = m_normalEvent.GetEvent();
        }
        else
        {
            pivot = UnityEngine.Random.Range(0, 100);
            if (pivot <= m_rate_wealthEvent)
            {
                pivot = UnityEngine.Random.Range(0, 10);
                bool status = (pivot <= m_wealth) ? true : false;

                eventRes = m_wealthEvent.GetEvent(status);
                playerEvent = eventRes.text;
            }
            else
            {
                pivot = UnityEngine.Random.Range(0, 10);
                bool status = (pivot <= m_strength) ? true : false;

                eventRes = m_wealthEvent.GetEvent(status);
                playerEvent = eventRes.text;
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
    }



    // Interface
    //=========================

    public int Wealth { get { return m_wealth; } set { m_wealth = value; } }
    public int Strength { get { return m_strength; } set { m_strength = value; } }
    public int IQ { get { return m_IQ; } set { m_IQ = value; } }


    public void SetActive(bool i_active)
    {
        gameObject.SetActive(i_active);
    }
}

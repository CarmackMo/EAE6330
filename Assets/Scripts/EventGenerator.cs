using UnityEngine;


public class EventGenerator : Singleton<EventGenerator>
{
    // Data
    //=========================

    [SerializeField] private NormalEventAsset normalEvent = null;

    private GameplayPanel gameplayPanel = null;

    private int m_wealth = 0;
    private int m_strength = 0;
    private int m_IQ = 0;


    private float lastAdd = 0.0f;
    private float coolDown = 3.0f;



    // Implementation
    //=========================

    void Start()
    {
        Init();
    }


    void Update()
    {
        
        if (Time.time - lastAdd >= coolDown) 
        {
            GenerateEvent();

            lastAdd = Time.time;    
        }
    }


    private void Init()
    {
        gameplayPanel = GameplayPanel.Instance;

        SetActive(false);
    }


    private void GenerateEvent()
    {
        float pivot = 0.0f;
        string playerEvent = "";
        pivot = Random.Range(0.0f, 100.0f);

        if (pivot < 100.0f)
        {
            playerEvent = normalEvent.GetEvent();
            gameplayPanel.AddEvent(playerEvent);
        }
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

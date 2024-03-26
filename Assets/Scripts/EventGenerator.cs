using UnityEngine;


public class EventGenerator : MonoBehaviour
{
    // Data
    //=========================

    public NormalEventAsset normalEvent = null;

    private GameplayPanel gameplayPanel = null;

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
}

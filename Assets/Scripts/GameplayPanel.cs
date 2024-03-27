using UnityEngine;


public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=========================

    [SerializeField] private GameObject eventScrollContent = null;

    [SerializeField] private EventItem eventItemPrefab = null;



    // Implementation
    //=========================

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        SetVisible(false);
    }



    // Interface
    //=========================

    public void AddEvent(string i_eventText)
    {
        EventItem eventItem = Instantiate(eventItemPrefab, eventScrollContent.transform);
        eventItem.SetEventText(i_eventText);
    }


    public void SetVisible(bool i_visible)
    {
        gameObject.SetActive(i_visible);
    }

}

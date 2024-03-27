using UnityEngine;
using TMPro;


public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=========================

    [SerializeField] private GameObject eventScrollContent = null;

    [SerializeField] private TextMeshProUGUI text_wealth = null;
    [SerializeField] private TextMeshProUGUI text_strength = null; 

    [SerializeField] private EventItem eventItemPrefab = null;

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


    public void UpdateUI()
    {
        text_wealth.text = s_eventGenerator.Wealth.ToString();
        text_strength.text = s_eventGenerator.Strength.ToString();
    }

}

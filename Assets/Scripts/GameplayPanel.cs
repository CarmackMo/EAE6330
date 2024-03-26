using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameplayPanel : Singleton<GameplayPanel>
{
    [SerializeField]
    private ScrollView eventScroll = null;

    [SerializeField]
    private GameObject eventScrollContent = null;

    [SerializeField]
    private EventItem eventItemPrefab = null;




    private void AddEvent(string eventText)
    {
        EventItem eventItem = Instantiate(eventItemPrefab, eventScrollContent.transform);
        eventItem.SetEventText(eventText);
    }
  

}

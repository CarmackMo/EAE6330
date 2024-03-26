using TMPro;
using UnityEngine;


public class EventItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI eventText = null;


    public void SetEventText(string text)
    {
        eventText.text = text;  
    }
}

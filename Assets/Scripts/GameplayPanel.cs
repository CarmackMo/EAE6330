using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=========================

    [SerializeField] private GameObject eventScrollContent = null;
    [SerializeField] private GameObject settingMenu = null;

    [SerializeField] private TextMeshProUGUI text_wealth = null;
    [SerializeField] private TextMeshProUGUI text_strength = null;
    [SerializeField] private TextMeshProUGUI text_perperty = null;
    [SerializeField] private TextMeshProUGUI text_tempWealth = null;
    [SerializeField] private TextMeshProUGUI text_tempStrenght = null;

    [SerializeField] private Button btn_pause = null;
    [SerializeField] private Button btn_resume = null;
    [SerializeField] private Button btn_addWealth = null;
    [SerializeField] private Button btn_subWealth = null;
    [SerializeField] private Button btn_addStrength = null;
    [SerializeField] private Button btn_subStrength = null;
    [SerializeField] private Button btn_confirm = null;

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

        btn_pause.onClick.AddListener(() => { OnClickButton(btn_pause); });
        btn_resume.onClick.AddListener(() => { OnClickButton(btn_resume); });
        btn_addWealth.onClick.AddListener(() => { OnClickButton(btn_addWealth); });
        btn_subWealth.onClick.AddListener(() => { OnClickButton(btn_subWealth); });
        btn_addStrength.onClick.AddListener(() => { OnClickButton(btn_addStrength); });
        btn_subStrength.onClick.AddListener(() => { OnClickButton(btn_subStrength); });
        btn_confirm.onClick.AddListener(() => { OnClickButton(btn_confirm); });
        btn_resume.gameObject.SetActive(false);

        settingMenu.gameObject.SetActive(false);    

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
        text_perperty.text = s_eventGenerator.Property.ToString();
        text_tempWealth.text = s_eventGenerator.TmpWealth.ToString();
        text_tempStrenght.text = s_eventGenerator.TmpStrength.ToString();
    }


    public void OnClickButton(Button i_btn)
    {
        if (i_btn == btn_pause)
        {
            s_eventGenerator.SetActive(false);
            btn_pause.gameObject.SetActive(false);
            btn_resume.gameObject.SetActive(true);
        }
        else if (i_btn == btn_resume)
        {
            s_eventGenerator.SetActive(true);
            btn_pause.gameObject.SetActive(true);
            btn_resume.gameObject.SetActive(false);
        }
        else if (i_btn == btn_addWealth) 
            s_eventGenerator.AddTmpWealth();
        else if (i_btn == btn_subWealth)
            s_eventGenerator.SubTmpWealth();
        else if (i_btn == btn_addStrength)
            s_eventGenerator.AddTmpStrength();
        else if (i_btn == btn_subStrength)
            s_eventGenerator.SubTmpStrength();
        else if (i_btn == btn_confirm)
            s_eventGenerator.ApplySetting();

        UpdateUI();
    }


    public void SetMenuVisible(bool i_visible)
    {
        settingMenu.gameObject.SetActive(i_visible);
    }

}

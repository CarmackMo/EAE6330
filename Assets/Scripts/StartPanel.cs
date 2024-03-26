using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Singleton<StartPanel>
{
    [SerializeField] private TextMeshProUGUI text_perperty = null;
    [SerializeField] private TextMeshProUGUI text_wealth = null;
    [SerializeField] private TextMeshProUGUI text_strength = null;
    [SerializeField] private TextMeshProUGUI text_IQ = null;


    [SerializeField] private Button btn_addWealth = null;
    [SerializeField] private Button btn_subWealth = null;
    [SerializeField] private Button btn_addStrength = null;
    [SerializeField] private Button btn_subStrength = null;
    [SerializeField] private Button btn_addIQ = null;
    [SerializeField] private Button btn_subIQ = null;

    [SerializeField] private Button btn_start = null;

    private GameInitializer s_gameInitializer = null;




    private void Start()
    {
        Init();
    }


    private void Init()
    {
        s_gameInitializer = GameInitializer.Instance;

        btn_addWealth.onClick.AddListener(() => { OnClickButton(btn_addWealth); });
        btn_subWealth.onClick.AddListener(() => { OnClickButton(btn_subWealth); });
        btn_addStrength.onClick.AddListener(() => { OnClickButton(btn_addStrength); });
        btn_subStrength.onClick.AddListener(() => { OnClickButton(btn_subStrength); });
        btn_addIQ.onClick.AddListener(() => { OnClickButton(btn_addIQ); });
        btn_subIQ.onClick.AddListener(() => { OnClickButton(btn_subIQ); });
        
        btn_start.onClick.AddListener(() => { OnClickButton(btn_start); });
    }


    private void OnClickButton(Button i_button)
    {
        if (i_button == btn_addWealth)
        { }
        else if (i_button == btn_subWealth)
        { }
        else if (i_button == btn_addStrength)
        { }
        else if (i_button == btn_subStrength)
        { }
        else if (i_button == btn_addIQ)
        { }
        else if (i_button == btn_subIQ)
        { }
        else if (i_button == btn_start)
        { }
    }

    
    public void UpdateUI(int i_property, int i_wealth, int i_strength, int i_IQ)
    {
        text_perperty.text = s_gameInitializer.TotalProperty.ToString();
        text_wealth.text = s_gameInitializer.Wealth.ToString();
        text_strength.text = s_gameInitializer.Strength.ToString();
        text_IQ.text = s_gameInitializer.IQ.ToString();


        text_perperty.text = i_property.ToString();
        text_wealth.text = i_wealth.ToString();
        text_strength.text = i_strength.ToString();
        text_IQ.text = i_IQ.ToString();
    }


}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Singleton<StartPanel>
{
    // Data
    //=========================

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



    // Implementation
    //=========================

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
            s_gameInitializer.AddWealth();
        else if (i_button == btn_subWealth)
            s_gameInitializer.SubWealth();
        else if (i_button == btn_addStrength)
            s_gameInitializer.AddStrength();
        else if (i_button == btn_subStrength)
            s_gameInitializer.SubStrength();
        else if (i_button == btn_addIQ)
            s_gameInitializer.AddIQ();
        else if (i_button == btn_subIQ)
            s_gameInitializer.SubIQ();
        else if (i_button == btn_start)
        {
            throw new NotImplementedException();
        }

        UpdateUI();
    }

    
    private void UpdateUI()
    {
        text_perperty.text = s_gameInitializer.TotalProperty.ToString();
        text_wealth.text = s_gameInitializer.Wealth.ToString();
        text_strength.text = s_gameInitializer.Strength.ToString();
        text_IQ.text = s_gameInitializer.IQ.ToString();
    }


}

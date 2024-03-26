using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitializer : Singleton<GameInitializer>
{
    [SerializeField] private int m_totalProperty = 0;


    private int m_wealth = 0;

    private int m_strength = 0;

    private int m_IQ = 0;


    private StartPanel s_startPanel = null;








    private void Start()
    {
        Init();
    }


    private void Init()
    {
        s_startPanel = StartPanel.Instance;
    }



    public int Wealth { get { return m_wealth; } private set { } }
    public int Strength { get { return m_strength; } private set { } }
    public int IQ { get { return m_IQ; } private set { } }



    public void AddIQ()
    {
        if (m_totalProperty > 0) 
        {
            m_IQ++;
            m_totalProperty--;
            s_startPanel.UpdateUI()
        }
    }

}

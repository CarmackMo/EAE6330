using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitializer : Singleton<GameInitializer>
{
    // Data
    //=========================

    [SerializeField] private int m_totalProperty = 0;


    private int m_wealth = 0;

    private int m_strength = 0;

    private int m_IQ = 0;

    private StartPanel s_startPanel = null;



    // Implementation
    //=========================

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        s_startPanel = StartPanel.Instance;
    }



    // Interface
    //=========================

    public int TotalProperty { get { return m_totalProperty; }  private set { } }
    public int Wealth { get { return m_wealth; } private set { } }
    public int Strength { get { return m_strength; } private set { } }
    public int IQ { get { return m_IQ; } private set { } }



    public void AddWealth()
    {
        if (m_totalProperty > 0)
        {
            m_wealth++;
            m_totalProperty--;
        }
    }


    public void SubWealth()
    {
        if (m_wealth > 0)
        {
            m_wealth--;
            m_totalProperty++; 
        }
    }


    public void AddStrength()
    {
        if (m_totalProperty > 0)
        {
            m_strength++;
            m_totalProperty--;
        }
    }


    public void SubStrength()
    {
        if (m_strength > 0)
        {
            m_strength--;
            m_totalProperty++;
        }
    }


    public void AddIQ()
    {
        if (m_totalProperty > 0) 
        {
            m_IQ++;
            m_totalProperty--;
        }
    }


    public void SubIQ()
    {
        if (m_IQ > 0)
        {
            m_IQ--;
            m_totalProperty++;
        }
    }

}

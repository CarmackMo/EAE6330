using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitializer : Singleton<GameInitializer>
{

    public int propertyPoint = 0;


    private int wealth = 0;

    private int strength = 0;

    private int IQ = 0;


    private StartPanel startPanel = null;



    private void Init()
    {
        startPanel = StartPanel.Instance;
    }



}

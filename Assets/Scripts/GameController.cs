using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private int m_width = 1920;
    [SerializeField]
    private int m_height = 1080;


    Vector2 m_downLeft = Vector2.zero;
    Vector2 m_topRight = Vector2.zero;

    public bool m_isSelectingWeapon = false;

    private InventoryPanel s_inventoryPanel = null;


    public Vector2 DownLeft { get { return m_downLeft; } }
    public Vector2 TopRight { get { return m_topRight; } }



    protected override void Start()
    {
        Screen.SetResolution(m_width, m_height, false);

        m_downLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -0));
        m_topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -0));

        s_inventoryPanel = InventoryPanel.Instance;
    }


    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            s_inventoryPanel.Show();
            m_isSelectingWeapon=true;
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            s_inventoryPanel.Hide();
            m_isSelectingWeapon=false;
        }
    }
}

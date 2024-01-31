using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private float m_boundary_h = 0.0f;
    [SerializeField]
    private float m_boundary_v = 0.0f;

    [SerializeField]
    private AudioSource m_audioSource;

    public bool m_isSelectingWeapon = false;

    private InventoryPanel s_inventoryPanel = null;




    public float Boundary_H{ get { return m_boundary_h; } }
    public float Boundary_V { get { return m_boundary_v; } }



    protected override void Start()
    {
        m_audioSource.Play();
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

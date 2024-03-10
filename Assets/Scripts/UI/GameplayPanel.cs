using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=================

    [SerializeField] private GameObject m_cursor = null;
    [SerializeField] private GameObject m_panel_gameover = null;
    [SerializeField] private GameObject m_panel_exit = null;

    [SerializeField] private TextMeshProUGUI m_text_stepCount = null;

    [SerializeField] private Button m_btn_exit = null;
    [SerializeField] private Button m_btn_restart = null;
    [SerializeField] private Button m_btn_nextLv = null;
    [SerializeField] private Button m_btn_exitConfirm = null;
    [SerializeField] private Button m_btn_exitCancel = null;



    // Implementations
    //=================

    protected override void Start()
    {
        base.Start();

        Init(); 
    }


    private void Init()
    {
        m_panel_gameover.SetActive(false);
        m_panel_exit.SetActive(false);
    }



    // Interfaces
    //=================

    public void UpdateCursorUI(Vector3 i_cursorPos_world)
    {
        Vector3 cursorPos_screen = Camera.main.WorldToScreenPoint(i_cursorPos_world);

        m_cursor.transform.position = cursorPos_screen;  
    }


    public void UpdateStepCountUI(int i_currStepCount)
    {
        m_text_stepCount.text = i_currStepCount.ToString();
    }


    public void ShowGameOverPanel()
    {

    }


    public void ShowExitPanel()
    {
        m_panel_exit.SetActive(!m_panel_exit.activeSelf);
    }
}

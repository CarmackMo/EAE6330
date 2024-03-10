using UnityEngine;
using TMPro;


public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=================

    [SerializeField] private GameObject m_cursor = null;

    [SerializeField] private TextMeshProUGUI m_text_stepCount = null;


    // Implementations
    //=================



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
}

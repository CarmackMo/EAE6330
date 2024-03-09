using UnityEngine;






public class GameplayPanel : Singleton<GameplayPanel>
{
    // Data
    //=================

    [SerializeField] private GameObject m_cursor = null;




    // Interfaces
    //=================

    public void UpdateCursorUI(Vector3 i_cursorPos_world)
    {
        Vector3 cursorPos_screen = Camera.main.WorldToScreenPoint(i_cursorPos_world);

        m_cursor.transform.position = cursorPos_screen;  
    }


}

using UnityEngine;
using UnityEngine.InputSystem;


public class GameController : Singleton<GameController>
{
    // Data
    //=================

    private int m_mapWidth = 0;
    private int m_mapHeight = 0;


    // Implementations
    //=================

    private Vector2 m_cursor = Vector2.zero;



    public void MoveCursor(InputAction i_action)
    {

    }


    // Interfaces
    //=================

    public int MapWidth { get { return m_mapWidth; } set { m_mapWidth = value; } }
    public int MapHeight { get { return m_mapHeight; } set { m_mapHeight = value; } }
}

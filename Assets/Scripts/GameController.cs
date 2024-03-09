using System;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    // Data
    //=================

    private int m_mapWidth = 0;
    private int m_mapHeight = 0;

    private Vector2Int m_endPoint = Vector2Int.zero;
    private Vector2Int m_cursor = Vector2Int.zero;
    private List<Tile_Base> m_tileMap = new List<Tile_Base>();

    private GameplayPanel s_gameplayPanel = null;


    // Implementations
    //=================

    protected override void Update()
    {
        base.Update();

        Debug.Log("Cursor: " + m_cursor);
    }


    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        s_gameplayPanel = GameplayPanel.Instance;
    }


    // Interfaces
    //=================

    public int MapWidth { get { return m_mapWidth; } set { m_mapWidth = value; } }
    public int MapHeight { get { return m_mapHeight; } set { m_mapHeight = value; } }


    public void MoveCursor(Vector2Int i_delta)
    {
        Vector2Int newPos = m_cursor + i_delta;
        newPos.x = Math.Clamp(newPos.x, 0, m_mapWidth - 1);
        newPos.y = Math.Clamp(newPos.y, 0, m_mapHeight - 1);

        m_cursor = newPos;


        Tile_Base currTile = m_tileMap[CoorToIdx(m_cursor.y, m_cursor.x)];
        s_gameplayPanel.UpdateCursorUI(currTile.transform.position);


        //===Temp===
        // 
        if (m_cursor == m_endPoint)
            Debug.Log("You Win!");
    }


    public void AddTile(Tile_Base i_tile)
    {
        m_tileMap.Add(i_tile);

        if (i_tile.GetType() == typeof(Tile_Start))
        {
            m_cursor = IdxToCoor(m_tileMap.Count - 1);

            s_gameplayPanel.UpdateCursorUI(i_tile.transform.position);
        }
        else if (i_tile.GetType() == typeof(Tile_End))
            m_endPoint = IdxToCoor(m_tileMap.Count - 1);
    }


    public int CoorToIdx(int i_row, int i_col)
    {
        return i_row * m_mapWidth + i_col;
    }


    public Vector2Int IdxToCoor(int i_idx)
    {
        return new Vector2Int(i_idx % m_mapWidth, i_idx / m_mapWidth);
    }
}

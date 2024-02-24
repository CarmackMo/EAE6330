using System;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    // Data
    //=================

    private int m_mapWidth = 0;
    private int m_mapHeight = 0;

    private Vector2 m_endPoint = Vector2.zero;
    private Vector2 m_cursor = Vector2.zero;
    private List<Tile_Base> m_tileMap = new List<Tile_Base>();


    // Implementations
    //=================

    protected override void Update()
    {
        base.Update();

        Debug.Log("Cursor: " + m_cursor);
    }



    // Interfaces
    //=================

    public int MapWidth { get { return m_mapWidth; } set { m_mapWidth = value; } }
    public int MapHeight { get { return m_mapHeight; } set { m_mapHeight = value; } }


    public void MoveCursor(Vector2 i_delta)
    {
        Vector2 newPos = m_cursor + i_delta;
        newPos.x = Math.Clamp(newPos.x, 0, m_mapWidth - 1);
        newPos.y = Math.Clamp(newPos.y, 0, m_mapHeight - 1);

        m_cursor = newPos;

        //===Temp===
        // 
        if (m_cursor == m_endPoint)
            Debug.Log("You Win!");
    }


    public void AddTile(Tile_Base i_tile)
    {
        m_tileMap.Add(i_tile);

        if (i_tile.GetType() == typeof(Tile_Start))
            m_cursor = IdxToCoor(m_tileMap.Count - 1);
        else if (i_tile.GetType() == typeof(Tile_End))
            m_endPoint = IdxToCoor(m_tileMap.Count - 1);
    }


    public int CoorToIdx(int i_row, int i_col)
    {
        return i_row * m_mapWidth + i_col;
    }


    public Vector2 IdxToCoor(int i_idx)
    {
        return new Vector2(i_idx % m_mapWidth, i_idx / m_mapWidth);
    }
}

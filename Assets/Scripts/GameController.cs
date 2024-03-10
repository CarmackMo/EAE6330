using System;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    // Data
    //=================

    private int m_mapWidth = 0;
    private int m_mapHeight = 0;
    private int m_stepCount = 0;

    private Vector2Int m_endPoint = Vector2Int.zero;
    private Vector2Int m_cursor = Vector2Int.zero;
    private List<Tile_Base> m_tileMap = new List<Tile_Base>();

    private GameInitializer s_gameInitializer = null;
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
        // Initialize static variables
        {
            s_gameplayPanel = GameplayPanel.Instance;
            s_gameInitializer = GameInitializer.Instance;
        }

        // Initialize member variables
        {
            m_mapWidth = s_gameInitializer.MapWidth;
            m_mapHeight = s_gameInitializer.MapHeight;
            m_stepCount = s_gameInitializer.StepCount;
        }

        // Initialize UI
        {
            s_gameplayPanel.UpdateStepCountUI(m_stepCount);
        }
    }


    // Interfaces
    //=================

    public void MoveCursor(Vector2Int i_delta)
    {
        Vector2Int newPos = m_cursor + i_delta;
        newPos.x = Math.Clamp(newPos.x, 0, m_mapWidth - 1);
        newPos.y = Math.Clamp(newPos.y, 0, m_mapHeight - 1);

        // Cursor can only move to other pos from a visited tile
        // Or move back to a visited tile
        if (GetCurrTile().State == TileState.Visited ||
            m_tileMap[CoorToIdx(newPos.y, newPos.x)].State == TileState.Visited)
        {
            m_cursor = newPos;
            s_gameplayPanel.UpdateCursorUI(GetCurrTile().transform.position);
        }

        //===Temp===
        // 
        if (m_cursor == m_endPoint)
            Debug.Log("You Win!");
    }


    public void ProbTile()
    {
        Debug.Log("Probe tile");

        Tile_Base target = GetCurrTile();
        if (target.State == TileState.Covered)
        {
            target.Reveal();
        }
        else if (target.State == TileState.Revealed &&
                 m_stepCount > 0)
        {
            target.Visit();
            m_stepCount--;
            s_gameplayPanel.UpdateStepCountUI(m_stepCount);
        }

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


    public Tile_Base GetCurrTile()
    {
        return m_tileMap[CoorToIdx(m_cursor.y, m_cursor.x)];
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

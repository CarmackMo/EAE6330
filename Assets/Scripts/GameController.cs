using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Command_Undo<TReceiver> : Command_Base<TReceiver> where TReceiver : class
{
    private Vector2Int m_origin = Vector2Int.zero;
    private Vector2Int m_curr = Vector2Int.zero;

    private Action<TReceiver, Vector2Int, Vector2Int> m_action_unvisit = null;

    public Command_Undo(TReceiver i_receiver, Action<TReceiver, Vector2Int, Vector2Int> i_action, Vector2Int i_origin, Vector2Int i_curr) :
        base(i_receiver, null)
    {
        m_origin = i_origin;
        m_curr = i_curr;
        m_action_unvisit = i_action;
    }

    public override void Execute()
    {
        m_action_unvisit.Invoke(m_receiver, m_origin, m_curr);
    }
}



public class GameController : Singleton<GameController>
{
    // Data
    //=================

    private int m_mapWidth = 0;
    private int m_mapHeight = 0;
    private int m_stepCount = 0;

    private Vector2Int m_endPoint = Vector2Int.zero;
    private Vector2Int m_cursor = Vector2Int.zero;
    private Vector2Int m_prevCursor = Vector2Int.zero;
    private List<Tile_Base> m_tileMap = new List<Tile_Base>();
    private Stack<Command_Undo<GameController>> m_undoCmdStack = new Stack<Command_Undo<GameController>>();

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


    private void UnVisit(Vector2Int i_origin, Vector2Int i_curr)
    {
        if (i_curr.y - 1 >= 0 && m_tileMap[CoorToIdx(i_curr.y - 1, i_curr.x)].State != TileState.Visited)
            m_tileMap[CoorToIdx(i_curr.y - 1, i_curr.x)].Unreveal();
        if (i_curr.y + 1 < m_mapHeight && m_tileMap[CoorToIdx(i_curr.y + 1, i_curr.x)].State != TileState.Visited)
            m_tileMap[CoorToIdx(i_curr.y + 1, i_curr.x)].Unreveal();
        if (i_curr.x - 1 >= 0 && m_tileMap[CoorToIdx(i_curr.y, i_curr.x - 1)].State != TileState.Visited)
            m_tileMap[CoorToIdx(i_curr.y, i_curr.x - 1)].Unreveal();
        if (i_curr.x + 1 < m_mapWidth && m_tileMap[CoorToIdx(i_curr.y, i_curr.x + 1)].State != TileState.Visited)
            m_tileMap[CoorToIdx(i_curr.y, i_curr.x + 1)].Unreveal();

        Tile_Base currTile = m_tileMap[CoorToIdx(i_curr.y, i_curr.x)];
        currTile.Unreveal();

        m_stepCount++;
        s_gameplayPanel.UpdateStepCountUI(m_stepCount);

        m_cursor = i_origin;
        s_gameplayPanel.UpdateCursorUI(GetCurrTile().transform.position);
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
            m_prevCursor = m_cursor;
            m_cursor = newPos;
            s_gameplayPanel.UpdateCursorUI(GetCurrTile().transform.position);
        }

        if (m_cursor == m_endPoint)
            s_gameplayPanel.ShowGameOverPanel();
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
        }
    }


    public void Undo()
    {
        Command_Undo<GameController> undoCmd = null;
        if (m_undoCmdStack.TryPeek(out undoCmd))
        {
            undoCmd.Execute();
            m_undoCmdStack.Pop();
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


    public void ExitGame()
    {
        Application.Quit();
    }


    public void ReloadCurrScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void DecreaseStepCount()
    {
        m_stepCount--;
        s_gameplayPanel.UpdateStepCountUI(m_stepCount);
    }


    public void RegisterUndoCmd()
    {
        Action<GameController, Vector2Int, Vector2Int> action = (i, j, k) => i.UnVisit(j, k);
        Command_Undo<GameController> undoCmd = new Command_Undo<GameController>(this, action, m_prevCursor, m_cursor);

        m_undoCmdStack.Push(undoCmd);
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

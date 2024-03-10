using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
struct MapRow
{
    public List<TileConfig> m_rowUnits;
};



public class GameInitializer : Singleton<GameInitializer>
{
    // Data
    //=================

    [SerializeField] private int m_width = 0;
    [SerializeField] private int m_height = 0;
    [SerializeField] private int m_stepCount = 0;

    [SerializeField] private Tile_Start m_prefab_startTile = null;
    [SerializeField] private Tile_End m_prefab_endTile = null;
    [SerializeField] private Tile_Norm m_prefab_normTile = null;

    [SerializeField] private List<MapRow> m_tileMap = new List<MapRow>();


    GameController s_gameController = null;



    // Implementations
    //=================

    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        // Initialize static variable
        {
            s_gameController = GameController.Instance;
        }

        // Initialize tile map
        {
            for (int row = 0; row < m_height; row++) 
            {
                for (int col = 0; col < m_width; col++) 
                {
                    TileConfig config = m_tileMap[row].m_rowUnits[col];

                    Tile_Base newTile = InstantiateTile(config, row, col);
                    s_gameController.AddTile(newTile);
                }
            }
        }

    }


    private Tile_Base InstantiateTile(TileConfig i_config, int i_row, int i_col)
    {
        Tile_Base prefab = null;
        if (i_config.m_tileType == TileType.Start)
            prefab = m_prefab_startTile;
        else if (i_config.m_tileType == TileType.End)
            prefab = m_prefab_endTile;
        else if (i_config.m_tileType == TileType.Normal)
            prefab = m_prefab_normTile;

        Tile_Base newTile = Instantiate(prefab);
        Vector2 tilePos = new Vector2(i_col * (newTile.Width + 0.25f), i_row * (newTile.Height + 0.25f));
        newTile.SetTilePos(tilePos);

        return newTile;
    }


    // Interfaces
    //=================

    public int MapWidth { get { return m_width; } private set { } }
    public int MapHeight { get { return m_height; } private set { } }
    public int StepCount { get { return m_stepCount; } private set { } }

}

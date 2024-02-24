using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable]
struct MapRow
{
    public List<TileConfig> m_rowUnits;
};



public class GameInitializer : Singleton<GameInitializer>
{
    [SerializeField] private int m_width = 0;
    [SerializeField] private int m_height = 0;

    [SerializeField] private Tile_Start m_prefab_startTile = null;
    [SerializeField] private Tile_End m_prefab_endTile = null;
    [SerializeField] private Tile_Norm m_prefab_normTile = null;

    [SerializeField] private List<MapRow> m_tileMap = new List<MapRow>();


    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        // Calculate the pixel-to-Unity-unit scale
        {
            float camDistance = Camera.main.transform.position.z;
            Vector3 topDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, camDistance));
            Vector3 downLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camDistance));
            float unit = Mathf.Abs(topDown.x) + Mathf.Abs(downLeft.x);
        }

        // Initialize tile map
        {
            for (int i  = 0; i < m_height; i++) 
            {
                for (int j = 0; j < m_width; j++) 
                {
                    TileConfig config = m_tileMap[i].m_rowUnits[j];

                    InstantiateTile(config, i, j);          
                }
            }

        }

    }


    private void InstantiateTile(TileConfig i_config, int i_row, int i_col)
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
        newTile.Init_Public(tilePos);
    }



}

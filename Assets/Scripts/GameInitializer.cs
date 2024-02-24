using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable]
struct MapRow
{
    public List<TileConfig> m_rowElement;
};



public class GameInitializer : MonoBehaviour
{
    [SerializeField] private int m_width = 0;
    [SerializeField] private int m_height = 0;

    [SerializeField] private List<MapRow> m_mapRows = new List<MapRow>();


    [SerializeField] private Tile_Norm m_prefab_normTile = null;
    [SerializeField] public tile m_prefab_


    private float m_pixelScale = 0f;    // pixel per unit



    private void Start()
    {
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
            m_pixelScale = Screen.width / unit;
        }


        // Initialize tile map
        {
            for (int i  = 0; i < m_height; i++) 
            {
                for (int j = 0; j < m_width; j++) 
                {
                    Tile_Base newTile = Instantiate(m_tile_norm);

                    Vector2 tilePos = new Vector2(j * (newTile.Width + 0.25f), i * (newTile.Height + 0.25f));
                    newTile.Init(tilePos);            
                }
            }

        }



    }

}

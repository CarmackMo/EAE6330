using System;
using UnityEngine;


public enum TileType
{
    None, Start, End, Normal, FireWall, 
}

public enum TileState
{
    Covered, Revealed,
}

[Serializable]
struct TileConfig
{
    public TileType m_tileType;
};



public class Tile_Base : MonoBehaviour
{
    // Data
    //=================

    protected Sprite m_sprite_rect = null;
    protected TileState m_tileState = TileState.Covered;
    
    [SerializeField] protected GameObject m_sprite_covered = null;
    [SerializeField] protected GameObject m_sprite_revealed = null;


    // Implementations
    //=================

    private void Awake()
    {
        Init();
    }


    virtual protected void Init()
    {
        // Initialize rect sprite 
        {
            Transform render = transform.Find("Sprite_Rect");
            m_sprite_rect = render.GetComponent<SpriteRenderer>().sprite;
        }

        // Initialize covered and revealed sprite
        {
            m_sprite_covered.SetActive(true);
            m_sprite_revealed.SetActive(false);
        }
    }


    // Interfaces
    //=================

    public float Width { get { return m_sprite_rect.rect.width / m_sprite_rect.pixelsPerUnit; } }
    public float Height { get { return m_sprite_rect.rect.height / m_sprite_rect.pixelsPerUnit; } }


    virtual public void Init_Public(Vector2 i_pos)
    {
        {
            transform.position = i_pos;
        }
    }

}

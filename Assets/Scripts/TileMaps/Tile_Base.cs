using System;
using UnityEngine;


public enum TileType
{
    None, Start, End, Normal, FireWall, Key
}

public enum TileState
{
    Covered, Revealed, Visited
}

[Serializable]
struct TileConfig
{
    public TileType m_tileType;
};


public class Command_OnVisit<TReceiver> : Command_Base<TReceiver> where TReceiver : class
{
    public Command_OnVisit(TReceiver i_receiver, Action<TReceiver> i_action) :
        base(i_receiver, i_action)
    { }
}




abstract public class Tile_Base : MonoBehaviour
{
    // Data
    //=================
    
    [SerializeField] protected GameObject m_sprite_covered = null;
    [SerializeField] protected GameObject m_sprite_revealed = null;
    [SerializeField] protected GameObject m_sprite_visited = null;

    protected Sprite m_sprite_rect = null;
    protected TileState m_tileState = TileState.Covered;
    protected Command_OnVisit<GameController> m_cmd_onVisit = null;


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
            m_sprite_visited.SetActive(false);
        }
    }


    // Interfaces
    //=================

    public float Width { get { return m_sprite_rect.rect.width / m_sprite_rect.pixelsPerUnit; } }
    public float Height { get { return m_sprite_rect.rect.height / m_sprite_rect.pixelsPerUnit; } }
    public TileState State { get { return m_tileState; } }


    public void SetTilePos(Vector2 i_pos)
    {
        transform.position = i_pos;
    }


    abstract public void Visit();


    public void Reveal()
    {
        m_sprite_covered.SetActive(false);
        m_sprite_revealed.SetActive(true);

        m_tileState = TileState.Revealed;
    }


    public void Unreveal()
    {
        m_sprite_covered.SetActive(true);
        m_sprite_revealed.SetActive(false);
        m_sprite_visited.SetActive(false);

        m_tileState = TileState.Covered;
    }

}

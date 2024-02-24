using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;


enum TileType
{
    None, Start, Normal, FireWall, 
}


[Serializable]
struct TileConfig
{
    public TileType m_tileType;
};



public class Tile_Base : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_render = null;

    public float Width { get { return m_render.sprite.rect.width / m_render.sprite.pixelsPerUnit; } }
    public float Height { get { return m_render.sprite.rect.height / m_render.sprite.pixelsPerUnit; } }




    virtual public void Init(Vector2 i_pos)
    {
        transform.position = i_pos;


    }

}

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;


enum TileType
{
    None, Start, End, Normal, FireWall, 
}


[Serializable]
struct TileConfig
{
    public TileType m_tileType;
};



public class Tile_Base : MonoBehaviour
{
    private Sprite m_sprite = null;

    public float Width { get { return m_sprite.rect.width / m_sprite.pixelsPerUnit; } }
    public float Height { get { return m_sprite.rect.height / m_sprite.pixelsPerUnit; } }





    private void Awake()
    {
        Init_Self();
    }


    private void Init_Self()
    {
        {
            Transform render = transform.Find("Sprite");
            m_sprite = render.GetComponent<SpriteRenderer>().sprite;
        }
    }


    virtual public void Init_Public(Vector2 i_pos)
    {
        {
            transform.position = i_pos;
        }
    }

}

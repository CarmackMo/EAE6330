using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct WeaponSlot
{
    public GameObject m_img_select;
    public Image m_img_weapon;
}



public class InventoryPanel : Singleton<InventoryPanel>
{
    [SerializeField] private List<WeaponSlot> m_weaponSlots = new List<WeaponSlot>();

    [SerializeField] private Sprite m_sprite_shotGun = null;
    [SerializeField] private Sprite m_sprite_laserGun = null;

    private Vector2 m_index = Vector2.zero;

    private GameController s_gameController = null;



    protected override void Start()
    {
        base.Start();
        s_gameController = GameController.Instance;
        Hide();
    }


    protected override void Update()
    {
        if (s_gameController.m_isSelectingWeapon)
        {
            if (Input.GetKeyUp(KeyCode.A) && m_index.x > -1)
                m_index.x--;
            else if (Input.GetKeyUp(KeyCode.D) && m_index.x < 1)
                m_index.x++;
            else if (Input.GetKeyUp(KeyCode.W) && m_index.y < 1)
                m_index.y++;
            else if (Input.GetKeyUp(KeyCode.S) && m_index.y > -1)
                m_index.y--;

            UpdateContentVisitability();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        }
    }


    public void Show()
    {
        gameObject.SetActive(true);
        m_index = Vector2.zero;
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateContent(int i_idx, Type i_weaponType)
    {
        if (i_weaponType == typeof(Weapon_ShotGun))
            m_weaponSlots[i_idx].m_img_weapon.sprite = m_sprite_shotGun;
    }


    private void UpdateContentVisitability()
    {
        foreach(WeaponSlot weapon in m_weaponSlots)
        {
            weapon.m_img_select.SetActive(false);
        }

        WeaponSlot weaponSlot = m_weaponSlots[GetWeaponIndex()];
        weaponSlot.m_img_select.SetActive(true);
    }


    private int GetWeaponIndex()
    {
        if (m_index == Vector2.up)
            return 1;
        else if (m_index == Vector2.down)
            return 2;
        else if (m_index == Vector2.left)
            return 3;
        else if (m_index == Vector2.right)
            return 4;
        else 
            return 0;
    }
    
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct WeaponSlot
{
    public GameObject m_img_select;
    public Image m_img_weapon;
}


public class Command_SelectWeapon<TReceiver> : Command_Base<TReceiver> where TReceiver : class
{

    private Action<TReceiver, int> m_action_selectWeapon = null;

    public Command_SelectWeapon(TReceiver i_receiver, Action<TReceiver, int> i_action)
        : base(i_receiver, null)
    {
        m_action_selectWeapon = i_action;
    }


    public void Execute(int i_idx)
    {
        m_action_selectWeapon.Invoke(m_receiver, i_idx);
    }
}



public class InventoryPanel : Singleton<InventoryPanel>
{
    [SerializeField] private List<WeaponSlot> m_weaponSlots = new List<WeaponSlot>();

    [SerializeField] private Sprite m_sprite_shotGun = null;
    [SerializeField] private Sprite m_sprite_laserGun = null;

    private Vector2 m_index = Vector2.zero;

    private Command_SelectWeapon<Player> m_cmd_selectWeapon = null;

    private GameController s_gameController = null;





    protected override void Start()
    {
        base.Start();
        s_gameController = GameController.Instance;
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
        m_cmd_selectWeapon.Execute(GetWeaponIndex());
        gameObject.SetActive(false);
    }


    public void InitCmd_SelectWeapon(Command_SelectWeapon<Player> i_cmd)
    {
        m_cmd_selectWeapon = i_cmd;
    }


    public void UpdateContent(int i_idx, Type i_weaponType)
    {
        if (i_weaponType == typeof(Weapon_ShotGun))
            m_weaponSlots[i_idx].m_img_weapon.sprite = m_sprite_shotGun;
        else if (i_weaponType == typeof(Weapon_LaserGun))
            m_weaponSlots[i_idx].m_img_weapon.sprite = m_sprite_laserGun;
        else if (i_weaponType == null)
            m_weaponSlots[i_idx].m_img_weapon.sprite = null;
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

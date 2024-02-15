using UnityEngine;
using TMPro;
using System;

public class GameplayPanel : Singleton<GameplayPanel>
{

    [SerializeField] private TextMeshProUGUI m_text_typeInfo = null;
    [SerializeField] private TextMeshProUGUI m_text_ammoInfo = null;


    protected override void Start() { }


    protected override void Update() { }


    public void UpdateWeaponTypeInfo(Type i_weaponType)
    {
        if (i_weaponType == typeof(Weapon_Default))
            m_text_typeInfo.text = "Default";
        else if (i_weaponType == typeof(Weapon_LaserGun))
            m_text_typeInfo.text = "Laser";
        else if (i_weaponType == typeof(Weapon_ShotGun))
            m_text_typeInfo.text = "ShotGun";

    }


    public void UpdateAmmoInfo(int i_ammo)
    {
        if (i_ammo == -1)
            m_text_ammoInfo.text = "INF";
        else
            m_text_ammoInfo.text = i_ammo.ToString();
    }
}

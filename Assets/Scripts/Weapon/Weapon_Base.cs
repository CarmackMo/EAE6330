using System;
using UnityEngine;


public class Command_OutOfAmmo<TReceiver> : Command_Base<TReceiver> where TReceiver : class
{

    private Action<TReceiver, Weapon_Base> m_action_outOfAmmo = null;

    public Command_OutOfAmmo(TReceiver i_receiver, Action<TReceiver, Weapon_Base> i_action) 
        : base(i_receiver, null)
    {
        m_action_outOfAmmo = i_action;
    }


    public void Execute(Weapon_Base weapon)
    {
        m_action_outOfAmmo.Invoke(m_receiver, weapon);
    }
}



public abstract class Weapon_Base : MonoBehaviour
{

    [SerializeField] protected int m_ammo = 0;

    [SerializeField] protected float m_cooldown = 0.0f;

    [SerializeField] protected Bullet m_bulletPrefab = null;

    [SerializeField] protected AudioSource m_audioSource = null;

    protected float m_lastFireTime = 0;

    protected Player m_player = null;

    protected Command_OutOfAmmo<Player> m_cmd_outOfAmmo = null;


    // Interface
    //------------

    public abstract void Init();


    public virtual void Fire()
    {
        float currTime = Time.time;
        if (currTime - m_lastFireTime >= m_cooldown)
        {
            ShootBullets();
            UpdateAmmo();

            m_audioSource.Play();
            m_lastFireTime = currTime;
        }
    }


    public void InitCmd_OnOutOfAmmo(Command_OutOfAmmo<Player> i_cmd)
    {
        m_cmd_outOfAmmo = i_cmd;
    }


    public int GetAmmo()
    {
        return m_ammo;
    }


    // Implementation
    //------------

    protected virtual void Start()
    {
        m_player = Player.Instance;
    }


    protected abstract void ShootBullets();


    protected abstract void UpdateAmmo();


    protected void OnOutOfAmmo()
    {
        m_cmd_outOfAmmo.Execute(this);        
    }

}

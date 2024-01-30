using UnityEngine;


abstract public class Weapon_Base : MonoBehaviour
{

    [SerializeField] protected float m_ammo = 0.0f;

    [SerializeField] protected float m_cooldown = 0.0f;

    [SerializeField] protected Bullet m_bulletPrefab = null;

    [SerializeField] protected AudioSource m_audioSource = null;

    protected float m_lastFireTime = 0.0f;



    public abstract void Init();


    protected abstract void ShootBullets();


    protected abstract void UpdateAmmo();


    public virtual void Fire()
    {
        float currTime = Time.time;
        if (currTime - m_lastFireTime >= m_cooldown)
        {
            ShootBullets();
            UpdateAmmo();

            m_audioSource.Play();
            m_lastFireTime = currTime;
            m_ammo--;
        }
    }




}

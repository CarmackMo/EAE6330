using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player_Laser : Bullet
{

    [SerializeField] AudioSource m_audioSource = null;


    protected void OnEnable()
    {
        m_audioSource.Play();
    }


    protected void OnDisable()
    {
        m_audioSource.Stop();
    }


    protected override void CleanUp()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {

    }
}

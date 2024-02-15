using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource m_source_boom = null;
    [SerializeField] private AudioSource m_source_click = null;
    [SerializeField] private AudioSource m_source_win = null;



    public void PlayBoomSound()
    {
        m_source_boom.Play();
    }


    public void PlayClickSound()
    {
        m_source_click.Play();
    }


    public void PlayWinSound()
    {
        m_source_win.Play();
    }


}

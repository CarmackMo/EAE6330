using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource m_audioSource1 = null;
    [SerializeField] private AudioSource m_AudioSource2 = null;


    protected override void Start() { }


    protected override void Update() { }


    public void PlayOneShot(AudioClip i_audio)
    {
        m_AudioSource2.PlayOneShot(i_audio);
    }



}

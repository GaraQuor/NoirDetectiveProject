﻿using UnityEngine;
using System.Collections;

public class S_AudioFX : MonoBehaviour {

    [Range(0,100)]
    public int m_VolumeMultiplier = 100;

    [HideInInspector]
    public float m_volume;

    void Start()
    {
        m_audioSource = GetComponents<AudioSource>();
    }

    void Update()
    {
        m_volume = (float)S_AudioManager.GlobalVolume / 100f * (float)S_AudioManager.FXVolume / 100f;

        for( int i = 0; i < m_audioSource.Length; i++ )
        {
            m_audioSource[ i ].volume = m_volume * (float)m_VolumeMultiplier / 100f;
        }

        //Debug.Log( m_volume + "   " + (float)S_AudioManager.GlobalVolume / 100f + "   " + (float)S_AudioManager.FXVolume / 100f + "   " + (float)m_VolumeMultiplier / 100f + "   " + m_audioSource.volume + "   "  );
    }


    private AudioSource[] m_audioSource;
}

﻿using UnityEngine;
using System.Collections;

public class S_AudioDialog : MonoBehaviour {

    public float m_volume;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
       
    }

    void Update()
    {
        m_volume = (float)S_AudioManager.GlobalVolume / 100f * (float)S_AudioManager.DialogVolume / 100f;

        m_audioSource.volume = m_volume;
    }


    private AudioSource m_audioSource;
}

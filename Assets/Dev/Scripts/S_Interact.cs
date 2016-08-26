﻿using UnityEngine;
using System.Collections;

public class S_Interact : MonoBehaviour
{
    
	void Start ()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.color = new Color( 1, 1, 1, 0.7f );
    }
	
	void Update ()
    {
	    if ( m_canInteract && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log( "Interact" );
            m_renderer.material.color = new Color( 1, 0, 0, 1f );
        }
	}
    
    void OnTriggerEnter(Collider collision)
    {
        m_canInteract = true;
        m_renderer.material.color = new Color( 1, 1, 1, 1f );
    }

    void OnTriggerExit(Collider collision)
    {
        m_canInteract = false;
        m_renderer.material.color = new Color( 1, 1, 1, 0.7f );
    }

    private bool m_canInteract;
    private Renderer m_renderer;
}
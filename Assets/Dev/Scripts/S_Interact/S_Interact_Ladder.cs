﻿using UnityEngine;
using System.Collections;

public class S_Interact_Ladder : S_Interact
{
    [HideInInspector]
    public GameObject m_PortalTop, m_PortalBottom;

    void Start()
    {
        Interact_Init();

        m_PortalTopTransform = m_PortalTop.GetComponent<Transform>();
        m_PortalBottomTransform = m_PortalBottom.GetComponent<Transform>();
    }

    protected override void On_Interact_Start( Collider _collision )
    {
        m_charact_controller.IsHidden = true;
        //m_charact_controller.IsClimbing = true;
        if(m_CharTransform.position.y < m_PortalTopTransform.position.y )
        {
            m_CharTransform.position = new Vector3( m_CharTransform.position.x, m_PortalTopTransform.position.y, m_CharTransform.position.z );
        }
        else if( m_CharTransform.position.y > m_PortalBottomTransform.position.y )
        {
            m_CharTransform.position = new Vector3( m_CharTransform.position.x, m_PortalBottomTransform.position.y, m_CharTransform.position.z );
        }



    }

    protected override void On_Interact_Leave( Collider _collision )
    {
        m_charact_controller.IsHidden = false;

        //m_charact_controller.IsClimbing = false;
    }

    private Transform m_PortalTopTransform, m_PortalBottomTransform;
}
﻿using UnityEngine;
using System.Collections;

public class S_Interact_Toggle_Pos : S_Interact
{
    public GameObject m_Portal_1, m_Portal_2;
    public bool m_BloquePlayer;

    void Start()
    {
        Interact_Init();

        m_Portal_1_Transform = m_Portal_1.GetComponent<Transform>();
        m_Portal_2_Transform = m_Portal_2.GetComponent<Transform>();
    }

    protected override void On_Interact_Start( Collider _collision )
    {
        float _dist1 = (m_CharTransform.position - m_Portal_1_Transform.position).magnitude;
        float _dist2 = (m_CharTransform.position - m_Portal_2_Transform.position).magnitude;
        
        if ( _dist1 > _dist2 )
        {
            m_charact_controller.Bloquer = false;
            m_CharTransform.position = m_Portal_1_Transform.position;
        }
        else
        {
            m_charact_controller.Bloquer = true;
            m_CharTransform.position = m_Portal_2_Transform.position;
        }
    }

    private Transform m_Portal_1_Transform, m_Portal_2_Transform;
}
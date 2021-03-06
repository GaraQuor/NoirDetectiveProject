﻿using UnityEngine;
using System.Collections;

public class S_Charact_AnimController : MonoBehaviour
{
    Transform m_Transform;
    Animator m_Animator;
    
    public GameObject m_SpineMechanism;
    public float m_SpeedX = 5f;
    
	void Start ()
    {
        m_Transform = GetComponent<Transform>();
        m_Animator = m_SpineMechanism.GetComponent<Animator>();
	}
	
	void Update ()
    {
        float moveX =  Input.GetAxis("Horizontal") * Time.deltaTime * m_SpeedX;
  
        m_Animator.SetFloat( "Speed", moveX );

        if( Input.GetAxis( "Horizontal" ) < 0 )
            m_Transform.localScale = new Vector3( -1 , m_Transform.localScale.y, m_Transform.localScale.z );
        else if( Input.GetAxis( "Horizontal" ) > 0 )
            m_Transform.localScale = new Vector3( 1, m_Transform.localScale.y, m_Transform.localScale.z );
        
        m_Transform.position = new Vector3( m_Transform.position.x + moveX, m_Transform.position.y, m_Transform.position.z);

        if(Input.GetKeyDown(KeyCode.K))
        {
            m_Animator.SetTrigger( "IsDead" );
        }
        if( Input.GetKeyDown( KeyCode.L ) )
        {
            m_Animator.SetInteger( "TakeDamage", Random.Range( 0, 2 ) );
            m_Animator.SetTrigger( "IsDamaged" );
        }

        if(Input.GetKeyDown( KeyCode.E ) )
        {
            // si dans dos de l'ennemi
            //      m_Animator.SetTrigger( "IsStabing" );
            // sinon
            m_Animator.SetInteger( "Attack", Random.Range( 0, 3 ) );
            m_Animator.SetTrigger( "IsAttacking" );
        }
        if( Input.GetKeyDown( KeyCode.R ) )
        {
            m_Animator.SetTrigger( "IsStomping" );
        }
    }
}

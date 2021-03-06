﻿using UnityEngine;
using System.Collections;

public class S_Enemy : MonoBehaviour
{
    public EnemyDirection m_direction;
    public Color m_PatrolColor = Color.white,
                 m_WarningColor = Color.yellow,
                 m_DetectColor = Color.red;

    public float SpeedDivisor = 30.0f;

    public EnemyType m_type = EnemyType.Mafia;

    public GameObject m_damagesIcons;

    void Start()
    {
        m_AI = GetComponent<S_Enemy_AI>();
        m_transform = GetComponent<Transform>();
        m_highlight = GetComponent<S_HighlightObject>();
        m_animator = GetComponentInChildren<Animator>();
        m_body = GetComponent<Rigidbody>();
        m_transform_Damage = m_damagesIcons.GetComponent<Transform>();
        m_ConeLight = m_highlightConeLigth.GetComponent<BoxCollider>();

        SetColor( m_PatrolColor );
    }

    void Update()
    {
        if( m_flip && Time.realtimeSinceStartup > m_conelightTimer )
        {
            m_flip = false;
            m_ConeLight.enabled = true;
        }
    }
	
    #region Direction
    public void SetDirection(EnemyDirection _direction)
    {
        m_direction = _direction;

        switch( m_direction )
        {
            case EnemyDirection.Left:
                m_transform.localScale = new Vector3( -1, m_transform.localScale.y, m_transform.localScale.z );
                m_transform_Damage.localScale = new Vector3( -1, m_transform_Damage.localScale.y, m_transform_Damage.localScale.z );
                break;
            case EnemyDirection.Right:
                m_transform.localScale = new Vector3( 1, m_transform.localScale.y, m_transform.localScale.z );
                m_transform_Damage.localScale = new Vector3( 1, m_transform_Damage.localScale.y, m_transform_Damage.localScale.z );
                break;
            default:
                break;
        }
        
        m_ConeLight.enabled = false;
        m_conelightTimer = Time.realtimeSinceStartup + 0.5f;
        m_flip = true;
    }

    public void InvertDirection()
    {
        switch( m_direction )
        {
            case EnemyDirection.Left:
                SetDirection( EnemyDirection.Right );
                break;
            case EnemyDirection.Right:
                SetDirection( EnemyDirection.Left );
                break;
            default:
                break;
        }
    }

    public void SetVelocity(float _dx, float _dy)
    {
        m_body.velocity = new Vector3( _dx, m_body.velocity.y );
        m_animator.SetFloat( "Speed", _dx / SpeedDivisor );
    }
    #endregion

    #region Color
    public SpriteRenderer m_highlightConeLigth;

    public void SetColor(Color _color)
    {
        m_highlightConeLigth.material.color = _color;
        m_highlight.m_HighlightColor = _color;
    }
    #endregion

    private float m_conelightTimer;
    private bool m_flip;
    private BoxCollider m_ConeLight;

    private Transform m_transform;
    private S_HighlightObject m_highlight;
    private Rigidbody m_body;
    private Animator m_animator;

    public S_Enemy_AI m_AI;
    public bool m_isKo;
    public bool m_isDead;

    private Transform m_transform_Damage;
}

public enum EnemyType
{
    Mafia,
    Cultist
}

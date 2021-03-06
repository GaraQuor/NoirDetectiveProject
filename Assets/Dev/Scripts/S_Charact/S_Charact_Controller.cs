﻿using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class S_Charact_Controller : MonoBehaviour
{
    public float Cam_Border_X = 8.0f;
    public float Cam_Border_Y = 8.0f;

    public float Far_Cam_Y_Max = 8.0f;
    public float Far_Cam_Y_Min = 0.0f;

    public float Cam_Min_Y = 1.0f; //Ground
    public float Cam_Min_X_Left = 1.0f;
    public float Cam_Min_X_Right = 1.0f;

    public float Cam_Speed = 3.0f;

    public bool IsHidden;

    public BoxCollider FightBox;

    public float SpeedDivisor = 40.0f;

    public GameObject SpineObject;
    public GameObject PunchSoundObject;

    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_body = GetComponent<Rigidbody>();
        m_cam_transform = Camera.main.transform;

        m_dir_R = false;
        m_dir_L = false;
        FightBox.enabled = false;

        IsHidden = false;
        m_canMove = true;
        Bloquer = true;

        m_madness = GetComponent<S_Charact_Madness>();
        m_highlight = GetComponent<S_HighlightObject>();
        m_Animator = GetComponentInChildren<Animator>();
        m_detect = GetComponentInChildren<S_Charact_Detect>();
    }

    void Update()
    {
        if( Input.GetButtonDown( "Escape" ) )
            S_SceneManager.Load_MainMenu();

        float dx = Input.GetAxis("Joy0_Move_X") * 10.0f;
        float dy = Input.GetAxis("Joy0_Move_Y") * 10.0f;

        if ( m_madness.ControlInverted )
        {
            float _dx = dx;
            dx = dy;
            dy = _dx;
        }

        if ( m_madness .AxisInverted )
        {
            dx = -dx;
            dy = -dy;
        }

        Update_Camera( dx, dy );

        if ( Bloquer )
        {
            dx = 0;
            dy = 0;
        }

        Update_Direction( dx , dy);
        Update_FightBox();
    }

    private void WalkAnime(float _dx)
    {
        m_Animator.SetFloat( "Speed", _dx * Time.deltaTime );
    }

    private void Update_Direction(float _dx, float _dy)
    {
        if( _dx > 0.0f && !m_dir_R )
        {
            m_dir_R = true;
            m_dir_L = false;

            m_transform.localScale = new Vector3( 1, m_transform.localScale.y, m_transform.localScale.z );
        }
        else
        if( _dx < 0.0f && !m_dir_L )
        {
            m_dir_L = true;
            m_dir_R = false;

            m_transform.localScale = new Vector3( -1, m_transform.localScale.y, m_transform.localScale.z );
        }
        
        if( m_canMove )
        {
            m_body.velocity = new Vector3( _dx, m_body.velocity.y, 0 );

            WalkAnime( _dx );
        }
    }

    private void Update_Camera(float _dx, float _dy)
    {
        if( Input.GetButton("Joy0_Shift") )
        {
            m_body.velocity = new Vector3( 0, 0 );
            WalkAnime( 0 );

            if( !m_lastShift )
            {
                m_lastCamPos = m_cam_transform.position;
                m_lastShift = true;
            }
            
            float new_cam_x = m_cam_transform.position.x + _dx * Time .deltaTime * Cam_Speed;
            float new_cam_y = m_cam_transform.position.y + _dy * Time .deltaTime * Cam_Speed;

            if( new_cam_x > m_transform.position.x + Cam_Border_X )
                new_cam_x = m_transform.position.x + Cam_Border_X;
            else
            if ( new_cam_x < m_transform.position.x - Cam_Border_X )
                new_cam_x = m_transform.position.x - Cam_Border_X;

            if( new_cam_y > m_transform.position.y + Far_Cam_Y_Max )
                new_cam_y = m_transform.position.y + Far_Cam_Y_Max;
            else
            if( new_cam_y < m_transform.position.y - Far_Cam_Y_Min )
                new_cam_y = m_transform.position.y - Far_Cam_Y_Min;

            if( new_cam_y < Cam_Min_Y )
                new_cam_y = Cam_Min_Y;

            if( new_cam_x < Cam_Min_X_Left )
                new_cam_x = Cam_Min_X_Left;
            if( new_cam_x > Cam_Min_X_Right )
                new_cam_x = Cam_Min_X_Right;

            m_cam_transform.position = new Vector3( new_cam_x, new_cam_y, m_cam_transform.position.z );

            m_canMove = false;
        }
        else
        if( Mathf.Abs( Input.GetAxis( "Joy0_Cam_X" ) ) > 0.1f || Mathf.Abs( Input.GetAxis( "Joy0_Cam_Y" ) ) > 0.1f )
        {
            m_body.velocity = new Vector3( 0, 0 );
            WalkAnime( 0 );

            if( !m_lastShift )
            {
                m_lastCamPos = m_cam_transform.position;
                m_lastShift = true;
            }

            float new_cam_x = m_cam_transform.position.x + Input.GetAxis( "Joy0_Cam_X" ) * 10.0f * Time .deltaTime * Cam_Speed;
            float new_cam_y = m_cam_transform.position.y + Input.GetAxis( "Joy0_Cam_Y" ) * -10.0f * Time .deltaTime * Cam_Speed;

            if( new_cam_x > m_transform.position.x + Cam_Border_X )
                new_cam_x = m_transform.position.x + Cam_Border_X;
            else
            if( new_cam_x < m_transform.position.x - Cam_Border_X )
                new_cam_x = m_transform.position.x - Cam_Border_X;

            if( new_cam_y > m_transform.position.y + Far_Cam_Y_Max )
                new_cam_y = m_transform.position.y + Far_Cam_Y_Max;
            else
            if( new_cam_y < m_transform.position.y - Far_Cam_Y_Min )
                new_cam_y = m_transform.position.y - Far_Cam_Y_Min;

            if( new_cam_y < Cam_Min_Y )
                new_cam_y = Cam_Min_Y;

            if( new_cam_x < Cam_Min_X_Left )
                new_cam_x = Cam_Min_X_Left;
            if( new_cam_x > Cam_Min_X_Right )
                new_cam_x = Cam_Min_X_Right;

            m_cam_transform.position = new Vector3( new_cam_x, new_cam_y, m_cam_transform.position.z );

            m_canMove = false;
        }
        else
        {
            if ( m_canMove )
            {
                if ( Mathf.Abs(m_body.velocity.x) < 0.1f && Mathf.Abs( m_body.velocity.y) < 0.1f)
                    m_camFollow = false;

                if( m_transform.position.x > m_cam_transform.position.x + Cam_Border_X || m_transform.position.x < m_cam_transform.position.x - Cam_Border_X )
                {
                    m_camFollow = true;
                }

                if ( m_camFollow )
                {
                    Vector3 newPos = new Vector3( m_transform.position.x, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );
                    
                    if( newPos.y < Cam_Min_Y )
                        newPos = new Vector3( newPos.x, Cam_Min_Y, newPos.z );

                    if( newPos.x < Cam_Min_X_Left )
                        newPos = new Vector3( Cam_Min_X_Left, newPos.y, newPos.z );
                    if( newPos.x > Cam_Min_X_Right )
                        newPos = new Vector3( Cam_Min_X_Right, newPos.y, newPos.z );

                    m_cam_transform.position = Vector3.Lerp( m_cam_transform.position, newPos, Time.deltaTime * Cam_Speed );
                }
                else
                {
                    Vector3 newPos = new Vector3( m_cam_transform.position.x, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );

                    m_cam_transform.position = Vector3.Lerp( m_cam_transform.position, newPos, Time.deltaTime * Cam_Speed );
                }
            }
            else
            {
                Vector3 newPos = new Vector3( m_transform.position.x, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );

                if( newPos.y < Cam_Min_Y )
                    newPos = new Vector3( newPos.x, Cam_Min_Y, newPos.z );

                if( newPos.x < Cam_Min_X_Left )
                    newPos = new Vector3( Cam_Min_X_Left, newPos.y, newPos.z );
                if( newPos.x > Cam_Min_X_Right )
                    newPos = new Vector3( Cam_Min_X_Right, newPos.y, newPos.z );

                m_cam_transform.position = Vector3.Lerp( m_cam_transform.position, newPos, Time.deltaTime * Cam_Speed );

                float lenght = Vector3.Distance(m_cam_transform.position, newPos);

                if( lenght < 0.5f )
                    m_canMove = true;
            }
            
            m_lastShift = false;
        }
    }

    private void Update_FightBox()
    {
        if( (Input.GetButtonDown("Joy0_Punch") || Input.GetButtonDown( "Joy0_Kill" )) && Time.realtimeSinceStartup > m_timerFightAnim && !Bloquer )
        {
            m_isStomp = Input.GetButtonDown( "Joy0_Kill" );

            if ( m_isStomp )
            {
                m_Animator.SetTrigger( "IsStomping" );

                m_timerFightAnim = Time.realtimeSinceStartup + 0.667f;
                m_timerFight = Time.realtimeSinceStartup + 0.28f;
            }
            else
            {
                if( Time.realtimeSinceStartup < m_timerFightAnim + 0.8f )
                {
                    m_animCount++;

                    if( m_animCount > 2 )
                        m_animCount = 0;
                }
                else
                    m_animCount = 0;
                
                if ( m_detect.m_AI != null && m_detect.m_AI.m_state != Enemy_AI_State.Attack && !m_detect.m_enemy.m_isKo )
                {
                    m_Animator.SetTrigger( "IsStabing" );
                    m_animCount = 0;
                }
                else
                {
                    m_Animator.SetInteger( "Attack", m_animCount );
                    m_Animator.SetTrigger( "IsAttacking" );
                }

                if( m_animCount == 0 )
                {
                    m_timerFightAnim = Time.realtimeSinceStartup + 0.667f;
                    m_timerFight = Time.realtimeSinceStartup + 0.22f;
                }
                else
                if( m_animCount == 1 )
                {
                    m_timerFightAnim = Time.realtimeSinceStartup + 0.333f;
                    m_timerFight = Time.realtimeSinceStartup + 0.10f;
                }
                else
                {
                    m_timerFightAnim = Time.realtimeSinceStartup + 1f;
                    m_timerFight = Time.realtimeSinceStartup + 0.22f;
                }
            }

            m_fightAnim = true;
        }

        if( m_fightAnim && Time.realtimeSinceStartup > m_timerFight )
        {
            FightBox.enabled = true;
            
            m_fightAnim = false;
            m_animEnd = true;

            m_timerAnimEnd = Time.realtimeSinceStartup + 0.1f;
        }

        if (m_animEnd && Time.realtimeSinceStartup > m_timerAnimEnd )
        {
            FightBox.enabled = false;
            m_animEnd = false;
        }
    }
    
   // [HideInInspector]
    public bool Bloquer;
    [HideInInspector]
    public S_HighlightObject m_highlight;
    [HideInInspector]
    public Animator m_Animator;

    private bool m_dir_R;
    private bool m_dir_L;

    private Vector3 m_lastCamPos;
    private bool m_lastShift;
    private bool m_canMove;

    private bool m_camFollow;

    private Transform m_transform;
    public Rigidbody m_body;
    private Transform m_cam_transform;
    private Highlighter m_highlightRight, m_highlightLeft;
    private S_Charact_Madness m_madness;

    private float m_timerFightAnim;
    private float m_timerFight;
    private float m_timerFightPunch;
    private bool m_fightAnim;
    public int m_animCount;

    private float m_timerAnimEnd;
    private bool m_animEnd;

    public bool m_isStomp;
    private S_Charact_Detect m_detect;
}

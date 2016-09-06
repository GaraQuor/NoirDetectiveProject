﻿using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class S_Charact_Controller : MonoBehaviour
{
    public float Cam_Border_X = 8.0f;
    public float Cam_Border_Y = 8.0f;

    public float Far_Cam_Y_Max = 8.0f;
    public float Far_Cam_Y_Min = 0.0f;

    public float Far_Cam_Move_Speed = 3.0f;

    public bool IsHidden;
    public bool IsClimbing;

    public Color HighlightColor = Color.black;
    public bool IsHighlighted = false;

    [HideInInspector]
    public BoxCollider FightBoxRight, FightBoxLeft ;
    [HideInInspector]
    public GameObject m_SpriteLeft, m_SpriteRight;

    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_body = GetComponent<Rigidbody>();
        m_cam_transform = Camera.main.transform;
        m_highlightRight = m_SpriteRight.AddComponent<Highlighter>();
        m_highlightLeft = m_SpriteLeft.AddComponent<Highlighter>();

        m_dir_R = false;
        m_dir_L = false;
        FightBoxLeft.enabled = false;
        FightBoxRight.enabled = false;
        IsHidden = false;
    }

    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * 10.0f;
        float dy = Input.GetAxis("Vertical") * 10.0f;

        Update_HighLight();
        Update_Direction( dx , dy);
        Update_Camera( dx, dy );
        Update_FightBox();
    }

    private void Update_Direction(float _dx, float _dy)
    {
        if( _dx > 0.0f && !m_dir_R )
        {
            m_dir_R = true;
            m_dir_L = false;

            m_SpriteLeft.SetActive( false );
            m_SpriteRight.SetActive( true );
        }
        else
        if( _dx < 0.0f && !m_dir_L )
        {
            m_dir_L = true;
            m_dir_R = false;

            m_SpriteLeft.SetActive( true );
            m_SpriteRight.SetActive( false );
        }
        
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if( IsClimbing && _dy > 0.0f )
            {
                m_body.useGravity = false;
            }
            else
            {
                m_body.useGravity = true;
                _dy = m_body.velocity.y;
            }

            m_body.velocity = new Vector3( _dx, _dy, 0 );
        }
    }

    private void Update_HighLight()
    {
        if( IsHighlighted && m_dir_L ) m_highlightLeft.ConstantOnImmediate( HighlightColor );
        else m_highlightLeft.ConstantOffImmediate();
        if( IsHighlighted && m_dir_R ) m_highlightRight.ConstantOnImmediate( HighlightColor );
        else m_highlightRight.ConstantOffImmediate();
    }

    private void Update_Camera(float _dx, float _dy)
    {
        if( Input.GetKey( KeyCode.LeftShift ) )
        {
            float new_cam_x = m_cam_transform.position.x + _dx * Time .deltaTime * Far_Cam_Move_Speed;
            float new_cam_y = m_cam_transform.position.y + _dy * Time .deltaTime * Far_Cam_Move_Speed;

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

            m_cam_transform.position = new Vector3( new_cam_x, new_cam_y, m_cam_transform.position.z );

            Last_Shift = true;
        }
        else
        {
            if( Last_Shift )
            {
                m_cam_transform.position = new Vector3( m_cam_transform.position.x, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z);
                Last_Shift = false;
            }

            if( m_transform.position.x > m_cam_transform.position.x + Cam_Border_X )
            {
                m_cam_transform.position = new Vector3( m_transform.position.x - Cam_Border_X, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );
            }
            else
            if( m_transform.position.x < m_cam_transform.position.x - Cam_Border_X )
            {
                m_cam_transform.position = new Vector3( m_transform.position.x + Cam_Border_X, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );
            }
            else
                m_cam_transform.position = new Vector3( m_cam_transform.position.x, m_transform.position.y + Cam_Border_Y, m_cam_transform.position.z );
        }
    }

    private void Update_FightBox()
    {
        if( Input.GetKeyDown( KeyCode.Q ) )
        {
            if( m_SpriteLeft.activeInHierarchy ) FightBoxLeft.enabled = true;
            if( m_SpriteRight.activeInHierarchy ) FightBoxRight.enabled = true;
        }
        else
        {
            if( m_SpriteLeft.activeInHierarchy ) FightBoxLeft.enabled = false;
            if( m_SpriteRight.activeInHierarchy ) FightBoxRight.enabled = false;
        }
    }

    private bool m_dir_R;
    private bool m_dir_L;

    private bool Last_Shift;

    private Transform m_transform;
    private Rigidbody m_body;
    private Transform m_cam_transform;
    private Highlighter m_highlightRight, m_highlightLeft;
}
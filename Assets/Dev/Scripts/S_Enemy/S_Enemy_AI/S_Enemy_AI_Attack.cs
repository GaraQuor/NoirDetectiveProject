﻿using UnityEngine;
using System.Collections;

public class S_Enemy_AI_Attack : MonoBehaviour
{
    public Renderer ConeLight;
    public float OutOfRange = 10.0f;

    void Start ()
    {
        m_enemy_AI = GetComponent<S_Enemy_AI>();
        m_transform = GetComponent<Transform>();
    }
    
    void Update ()
    {
        if ( m_enemy_AI.m_state == EnemyAction.Attack )
        {
            S_MadnessBar.progress += 0.015f * Time.deltaTime;

            float _dist = Mathf.Abs( m_transform.position.x - m_player_transform.position.x );

            if( _dist > 2.6f )
            {
                if( m_transform.position.x - m_player_transform.position.x > 0 )
                {
                    m_transform.localScale = new Vector3( -1, 1, 1 );
                    m_transform.position = new Vector3( m_transform.position.x - 5.0f * Time.deltaTime, m_transform.position.y, m_transform.position.z );
                }
                else
                {
                    m_transform.localScale = new Vector3( 1, 1, 1 );
                    m_transform.position = new Vector3( m_transform.position.x + 5.0f * Time.deltaTime, m_transform.position.y, m_transform.position.z );
                }
            }

            if ( _dist > OutOfRange )
            {
                Debug.Log( "Is lost !" );
                m_lastposx = m_player_transform.position.x;
                ConeLight.material.color = Color.white;

                m_enemy_AI.Start_LookAround();
            }
        }
    }

    public void Attack_Player(Transform _player_transform)
    {
        ConeLight.material.color = Color.red;

        m_player_transform = _player_transform;
        m_enemy_AI.m_state = EnemyAction.Attack;

        //Debug.Log( "Attack !" );
    }

    private S_Enemy_AI m_enemy_AI;

    private Transform m_transform;
    private Transform m_player_transform;
    
    private float m_lastposx;
}

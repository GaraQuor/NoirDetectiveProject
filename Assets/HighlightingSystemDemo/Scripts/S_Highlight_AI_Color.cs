﻿using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class S_Highlight_AI_Color : MonoBehaviour {


    public GameObject m_SpriteLeft, m_SpriteRight;
    public Color HighlightColor = Color.black;
    public bool IsHighlighted = false;

    void Awake()
    {
        m_enemy_AI = GetComponent<S_Enemy_AI>();
        m_highlightRight = m_SpriteRight.AddComponent<Highlighter>();
        m_highlightLeft = m_SpriteLeft.AddComponent<Highlighter>();

    }

    // Update is called once per frame
    void Update()
    {

        if( IsHighlighted && m_enemy_AI.m_direction == EnemyDirection.Left) m_highlightLeft.ConstantOnImmediate( HighlightColor );
        else m_highlightLeft.ConstantOffImmediate();

        if( IsHighlighted && m_enemy_AI.m_direction == EnemyDirection.Right ) m_highlightRight.ConstantOnImmediate( HighlightColor );
        else m_highlightRight.ConstantOffImmediate();


    }

    private S_Enemy_AI m_enemy_AI;
    private Highlighter m_highlightRight, m_highlightLeft;
}

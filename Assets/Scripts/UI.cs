using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_Sprites;
    [SerializeField] private Color[] m_Colors;
    [SerializeField] private Image m_markImage;
    [SerializeField] private UnityEventInt UpdateX;
    [SerializeField] private UnityEventInt Update0;
    public int ScoreX { get; private set; }
    public int Score0 { get; private set; }

    public void SetMarkImage(bool move)
    {
        if (m_Sprites != null)
        {
            m_markImage.color = move ? m_Colors[0] : m_Colors[1];
            m_markImage.sprite = move ? m_Sprites[0] : m_Sprites[1];
        }
    }

    public void AddScore(bool playerX)
    {
        if (playerX)
        {
            ScoreX += 1;
            UpdateX.Invoke(ScoreX);
        }
        else
        {
            Score0 += 1;
            Update0.Invoke(Score0);
        }
    }

    private void OnEnable()
    {
        GameController.OnWin += AddScore;
    }

    private void OnDisable()
    {
        GameController.OnWin -= AddScore;
    }
}

[System.Serializable]
public class UnityEventInt : UnityEvent<int> { }

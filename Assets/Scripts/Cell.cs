using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_mark;
    [SerializeField] private Image m_circle;
    [SerializeField] private Image m_cross;

    private bool IsEmpty()
    {
        return m_mark.text == "";
    }

    public void SetMark(bool player1)
    {
        if (IsEmpty())
        {
            m_mark.text = player1 ? "X" : "0";
            if (player1)
            {
                m_cross.gameObject.SetActive(true);
            }
            else m_circle.gameObject.SetActive(true);
        }
    }

    public string GetMark()
    {
        return GetComponentInChildren<Text>().text;
    }

    public void OnClick(Cell cell)
    {
        GameController.Instance.Move(this);
    }
}

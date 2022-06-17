using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_mark;
    private bool IsEmpty()
    {
        if (m_mark.text != "")
        {
            return false;
        }
        return true;
    }

    public void SetMark(bool player1)
    {
        if (IsEmpty())
        {
            m_mark.text = player1 ? "X" : "0";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntToText : MonoBehaviour
{
    [SerializeField] private Text m_Text;
    public void SetInt(int value)
    {
        m_Text.text = value.ToString();
    }
}

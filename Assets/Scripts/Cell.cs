using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private const float RectTransformRatio = 0.7f;
    [SerializeField] private Text m_Mark;
    [SerializeField] private Image m_Circle;
    [SerializeField] private Image m_Cross;
    [SerializeField] private RectTransform m_CircleRectTransform;
    [SerializeField] private RectTransform m_CrossRectTransform;
    [SerializeField] private Color m_DefaultColor;
    [SerializeField] private Button m_Button;

    private void Start()
    {
        m_CircleRectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta * RectTransformRatio;
        m_CrossRectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta * RectTransformRatio;
    }

    private bool IsEmpty()
    {
        return m_Mark.text == "";
    }

    public void SetMark(bool player1)
    {
        if (IsEmpty())
        {
            m_Mark.text = player1 ? "X" : "0";
            if (player1)
            {
                m_Cross.gameObject.SetActive(true);
            }
            else m_Circle.gameObject.SetActive(true);

            m_Button.interactable = false;
        }
    }

    public void SetEmpty()
    {
        m_Mark.text = "";
        m_Cross.gameObject.SetActive(false);
        m_Circle.gameObject.SetActive(false);
        GetComponent<Image>().color = m_DefaultColor;
        m_Button.interactable = true;
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

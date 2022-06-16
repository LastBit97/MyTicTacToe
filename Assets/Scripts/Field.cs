using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private RectTransform m_FieldRectTransform;
    [SerializeField] private Cell m_cellPrefab;

    [SerializeField] private int m_cellCount = 3;
    [SerializeField] private float m_cellSpacing = 5;
    private float m_cellSize = 100;

    private Cell[,] m_cellsArray;

    private void Awake()
    {
        SetCellSizeAndSpacing();

        if (m_cellsArray == null)
        {
            CreateField();
        }
    }

    private void SetCellSizeAndSpacing()
    {
        if (m_cellCount >= 3 && m_cellCount <= 7)
        {
            m_cellSize = 300 - (m_cellCount - 3) * 50;
        }
    }

    private void CreateField()
    {
        m_cellsArray = new Cell[m_cellCount, m_cellCount];

        float fieldSize = m_cellCount * (m_cellSize + m_cellSpacing) - m_cellSpacing;
        m_FieldRectTransform.sizeDelta = new Vector2(fieldSize, fieldSize);

        float startX = -(fieldSize / 2) + (m_cellSize / 2);
        float startY = (fieldSize / 2) - (m_cellSize / 2);

        for (int x = 0; x < m_cellCount; x++)
        {
            for (int y = 0; y < m_cellCount; y++)
            {
                var cell = Instantiate(m_cellPrefab, transform, false);
                var position = new Vector2(startX + (x * (m_cellSize + m_cellSpacing)), startY - (y * (m_cellSize + m_cellSpacing)));
                cell.GetComponent<RectTransform>().sizeDelta = new Vector2(m_cellSize, m_cellSize);
                cell.transform.localPosition = position;

                m_cellsArray[x, y] = cell;
            }
        }

    }
}

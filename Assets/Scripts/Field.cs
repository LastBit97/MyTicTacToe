using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Field : MonoBehaviour
{
    private const int DefaultCellCount = 3;
    [SerializeField] private RectTransform m_FieldRectTransform;
    [SerializeField] private Cell m_CellPrefab;

    [SerializeField] private int m_CellCount = DefaultCellCount;
    private float m_CellSpacing = 7;
    private float m_CellSize = 100;

    private Cell[,] m_CellsArray;

    public int CellCount { get => m_CellCount; set => m_CellCount = value; }

    private void Awake()
    {
        SetCellSizeAndSpacing();

        if (m_CellsArray == null)
        {
            CreateField();
        }
    }

    private void SetCellSizeAndSpacing()
    {
        if (m_CellCount >= DefaultCellCount && m_CellCount <= 7)
        {
            m_CellSpacing = 9 - (m_CellCount - DefaultCellCount);
            m_CellSize = 300 - (m_CellCount - DefaultCellCount) * 50;
        }
    }

    private void CreateField()
    {
        m_CellsArray = new Cell[m_CellCount, m_CellCount];

        float fieldSize = m_CellCount * (m_CellSize + m_CellSpacing) - m_CellSpacing;
        m_FieldRectTransform.sizeDelta = new Vector2(fieldSize, fieldSize);

        float startX = -(fieldSize / 2) + (m_CellSize / 2);
        float startY = (fieldSize / 2) - (m_CellSize / 2);

        for (int x = 0; x < m_CellCount; x++)
        {
            for (int y = 0; y < m_CellCount; y++)
            {
                var cell = Instantiate(m_CellPrefab, transform, false);
                var position = new Vector2(startX + (x * (m_CellSize + m_CellSpacing)), startY - (y * (m_CellSize + m_CellSpacing)));
                cell.GetComponent<RectTransform>().sizeDelta = new Vector2(m_CellSize, m_CellSize);
                cell.transform.localPosition = position;

                m_CellsArray[x, y] = cell;
            }
        }
    }

    public Cell[,] GetCells()
    {
        return m_CellsArray;
    }
}

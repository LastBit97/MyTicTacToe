using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private Field m_field;

    private bool m_isGameOver;
    private Cell[,] m_cells;
    private bool m_playerX = true; // true - X, false - 0

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Move(Cell cell)
    {
        if (m_isGameOver)
        {
            return;
        }

        cell.SetMark(m_playerX);
        CheckForWin(cell.GetMark());

        m_playerX = !m_playerX;
    }

    public void CheckForWin(string mark)
    {
        if (m_cells == null)
        {
            m_cells = m_field.GetCells();
        }

        //Диагонали

        int countForWin = m_field.CellCount;
        int countDiagMain = 0, countDiagSecond = 0;

        for (int i = 0; i < m_field.CellCount; i++)
        {
            if (m_cells[i, i].GetMark() == mark)
                countDiagMain++;
            if (m_cells[i, m_field.CellCount - i - 1].GetMark() == mark)
                countDiagSecond++;
        }
        if (countDiagMain == countForWin || countDiagSecond == countForWin)
            Debug.Log("Win");

        //Столбцы и строки

        int countCol = 0, countRow = 0;

        for (int x = 0; x < m_field.CellCount; x++)
        {
            countCol = 0;
            countRow = 0;
            for (int y = 0; y < m_field.CellCount; y++)
            {
                if (m_cells[x, y].GetMark() == mark)
                    countCol++;
                if (m_cells[y, x].GetMark() == mark)
                    countRow++;
            }
            if (countCol == countForWin || countRow == countForWin)
                Debug.Log("Win");
        }
        

    }

}

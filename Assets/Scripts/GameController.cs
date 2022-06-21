using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

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

        var mark = cell.GetMark();

        var winLine = CheckForWin(mark);

        if (winLine.Count > 0)
        {
            Win(mark, winLine);
        }

        m_playerX = !m_playerX;
    }

    private void Win(string mark, List<Point> winLine)
    {
        Debug.Log(mark == "X" ? "Победил крестик" : "Победил нолик");

        var color = Color.green;

        foreach (var point in winLine)
        {
            var x = point.X;
            var y = point.Y;
            m_cells[x,y].GetComponent<Image>().color = color;
        }
    }

    public List<Point> CheckForWin(string mark)
    {
        m_cells ??= m_field.GetCells();

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

        var points = new List<Point>();

        if (countDiagMain == countForWin)
        {
            for (int i = 0; i < m_field.CellCount; i++)
            {
                points.Add(new Point(i, i));
            }
        }

        if (countDiagSecond == countForWin)
        {
            for (int i = 0; i < m_field.CellCount; i++)
            {
                points.Add(new Point(i, m_field.CellCount - i - 1));
            }
        }

        //Столбцы и строки

        for (int x = 0; x < m_field.CellCount; x++)
        {
            var countCol = 0;
            var countRow = 0;
            for (int y = 0; y < m_field.CellCount; y++)
            {
                if (m_cells[x, y].GetMark() == mark)
                    countCol++;
                if (m_cells[y, x].GetMark() == mark)
                    countRow++;
            }

            if (countCol == countForWin)
            {
                for (int i = 0; i < m_field.CellCount; i++)
                {
                    points.Add(new Point(x, i));
                }
            }

            if (countRow != countForWin) continue;
            {
                for (int i = 0; i < m_field.CellCount; i++)
                {
                    points.Add(new Point(i, x));
                }
            }
        }

        return points;
    }

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


}

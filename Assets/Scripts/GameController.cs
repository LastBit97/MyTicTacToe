using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private Field m_Field;
    [SerializeField] private UI m_Ui;

    private Cell[,] m_Cells;
    private bool m_PlayerX = true; // true - X, false - 0
    [SerializeField] private int m_CellsLeft;

    public static event Action<bool> OnWin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        m_Cells ??= m_Field.GetCells();
        m_CellsLeft = m_Cells.Length;

        RandomTurn();
    }

    private void RandomTurn()
    {
        m_PlayerX = Random.Range(0, 2) == 0;
        m_Ui.SetMarkImage(m_PlayerX);
    }

    public void Move(Cell cell)
    {
        cell.SetMark(m_PlayerX);

        m_CellsLeft -= 1;

        var mark = cell.GetMark();

        var winLine = CheckForWin(mark);

        if (winLine.Count > 0)
        {
            Win(mark, winLine);
            return;
        }

        if (m_CellsLeft <= 0)
        {
            Draw();
            return;
        }

        ChangeTurn();
    }

    private void ChangeTurn()
    {
        m_PlayerX = !m_PlayerX;
        m_Ui.SetMarkImage(m_PlayerX);
    }

    private void Draw()
    {
        Debug.Log( "Ничья");
        Invoke(nameof(Restart), 1f);
    }

    private void Win(string mark, List<Point> winLine)
    {
        Debug.Log(mark == "X" ? "Победил крестик" : "Победил нолик");

        var color = Color.green;

        foreach (var point in winLine)
        {
            var x = point.X;
            var y = point.Y;
            m_Cells[x,y].GetComponent<Image>().color = color;
        }

        OnWin?.Invoke(mark == "X");
        DisableInteractable();
        Invoke(nameof(Restart), 1f);
    }

    private void DisableInteractable()
    {
        foreach (var cell in m_Cells)
        {
            cell.GetComponent<Button>().interactable = false;
        }
    }

    private void Restart()
    {
        foreach (var cell in m_Cells)
        {
            cell.SetEmpty();
        }
        m_CellsLeft = m_Cells.Length;
        RandomTurn();
    }

    /// <summary>
    /// Возвращает список индексов ячеек победившей линии. Если не победили возвращает список без элементов
    /// </summary>
    /// <param name="mark">Х или 0</param>
    /// <returns>Индексы ячеек победившей линии</returns>
    public List<Point> CheckForWin(string mark)
    {
        //Диагонали

        int countForWin = m_Field.CellCount;
        int countDiagMain = 0, countDiagSecond = 0;

        for (int i = 0; i < m_Field.CellCount; i++)
        {
            if (m_Cells[i, i].GetMark() == mark)
                countDiagMain++;
            if (m_Cells[i, m_Field.CellCount - i - 1].GetMark() == mark)
                countDiagSecond++;
        }

        var points = new List<Point>();

        if (countDiagMain == countForWin)
        {
            for (int i = 0; i < m_Field.CellCount; i++)
            {
                points.Add(new Point(i, i));
            }
        }

        if (countDiagSecond == countForWin)
        {
            for (int i = 0; i < m_Field.CellCount; i++)
            {
                points.Add(new Point(i, m_Field.CellCount - i - 1));
            }
        }

        //Столбцы и строки

        for (int x = 0; x < m_Field.CellCount; x++)
        {
            var countCol = 0;
            var countRow = 0;
            for (int y = 0; y < m_Field.CellCount; y++)
            {
                if (m_Cells[x, y].GetMark() == mark)
                    countCol++;
                if (m_Cells[y, x].GetMark() == mark)
                    countRow++;
            }

            if (countCol == countForWin)
            {
                for (int i = 0; i < m_Field.CellCount; i++)
                {
                    points.Add(new Point(x, i));
                }
            }

            if (countRow == countForWin)
            {
                for (int i = 0; i < m_Field.CellCount; i++)
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

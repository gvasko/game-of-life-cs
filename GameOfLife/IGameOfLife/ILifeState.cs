﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    public struct Cell
    {
        public static Cell[] AsCellArray(int[,] intArray)
        {
            Cell[] cellArray = new Cell[intArray.GetLength(0)];
            for (int i = 0; i < intArray.GetLength(0); i++)
            {
                    cellArray[i] = new Cell(intArray[i, 0], intArray[i, 1]);
            }
            return cellArray;
        }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static int Comparison(Cell a, Cell b)
        {
            if (a.y != b.y)
            {
                return a.y - b.y;
            }
            else
            {
                return a.x - b.x;
            }
        }

        private int x;
        public int X { get { return x; } }

        private int y;
        public int Y { get { return y; } }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public override int GetHashCode()
        {
            return (17 * 23 + x) * 23 + y;
        }
    }

    public delegate void CellVisitorDelegate(Cell cell);

    // TODO: how to test struct?
    public struct BoundingBox
    {
        public BoundingBox(int x1, int y1, int x2, int y2)
        {
            minPoint = new Cell(Math.Min(x1, x2), Math.Min(y1, y2));
            maxPoint = new Cell(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        public BoundingBox(Cell[] cells)
        {
            if (cells.Length == 0)
            {
                minPoint = new Cell(0, 0);
                maxPoint = new Cell(0, 0);
            }
            else
            {
                minPoint = new Cell(cells.Select(c => c.X).Min(), cells.Select(c => c.Y).Min());
                maxPoint = new Cell(cells.Select(c => c.X).Max(), cells.Select(c => c.Y).Max());
            }
        }

        private Cell minPoint;
        public Cell MinPoint { get { return minPoint; } }

        private Cell maxPoint;
        public Cell MaxPoint { get { return maxPoint; } }

        public int Width { get { return maxPoint.X - minPoint.X + 1; } }
        public int Height { get { return maxPoint.Y - minPoint.Y + 1; } }

        public bool IsInside(Cell cell)
        {
            return
                minPoint.X <= cell.X && cell.X <= maxPoint.X &&
                minPoint.Y <= cell.Y && cell.Y <= maxPoint.Y;
        }

        public void VisitEachCell(CellVisitorDelegate visitor)
        {
            for (int x = MinPoint.X - 1; x <= MaxPoint.X + 1; x++)
            {
                for (int y = MinPoint.Y - 1; y <= MaxPoint.Y + 1; y++)
                {
                    Cell currentCell = new Cell(x, y);
                    visitor(currentCell);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[{0} .. {1}]", minPoint.ToString(), maxPoint.ToString());
        }

    }

    public delegate void CellStatusVisitorDelegate(Cell cell, CellStatus status);

    public interface ILifeState
    {
        BoundingBox BoundingBox { get; }

        CellStatus GetCellStatus(Cell cell);

        void VisitLiveCells(CellStatusVisitorDelegate visitor);

        void VisitEachCell(CellStatusVisitorDelegate visitor);

        void VisitEachNeightboursOfCell(Cell cell, CellStatusVisitorDelegate visitor);
    }
}

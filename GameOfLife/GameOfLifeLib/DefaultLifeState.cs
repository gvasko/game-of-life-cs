using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{

    internal class DefaultLifeState : ILifeState
    {
        private HashSet<Cell> liveCells;
        private Cell[] originalCells;
        private BoundingBox boundingBox;

        internal DefaultLifeState(Cell[] liveCells)
        {
            this.liveCells = new HashSet<Cell>();
            List<Cell> cellsWithoutDuplication = new List<Cell>();
            foreach (Cell liveCell in liveCells)
            {
                if (!this.liveCells.Contains(liveCell))
                {
                    this.liveCells.Add(liveCell);
                    cellsWithoutDuplication.Add(liveCell);
                }
            }
            cellsWithoutDuplication.Sort(Cell.Comparison);
            originalCells = cellsWithoutDuplication.ToArray();
            boundingBox = new BoundingBox(originalCells);
        }

        public BoundingBox BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }

        public CellStatus GetCellStatus(Cell cell)
        {
            if (BoundingBox.IsInside(cell))
            {
                if (liveCells.Contains(cell))
                {
                    return CellStatus.Alive;
                }
            }
            return CellStatus.Dead;
        }

        public void VisitLiveCells(CellStatusVisitorDelegate visitor)
        {
            foreach (Cell alive in liveCells)
            {
                visitor(alive, CellStatus.Alive);
            }
        }

        public void VisitEachCell(CellStatusVisitorDelegate visitor)
        {
            BoundingBox.VisitEachCell(delegate(Cell cell) {
                visitor(cell, GetCellStatus(cell));
            });
        }

        public void VisitEachNeightboursOfCell(Cell cell, CellStatusVisitorDelegate visitor)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x ==0 && y == 0)
                    {
                        continue;
                    }

                    Cell currentCell = new Cell(cell.X + x, cell.Y + y);
                    visitor(currentCell, GetCellStatus(currentCell));
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            DefaultLifeState that = obj as DefaultLifeState;

            if (that == null)
            {
                return false;
            }

            if (this.liveCells.Count != that.liveCells.Count)
            {
                return false;
            }

            foreach (Cell thisCell in this.liveCells)
            {
                if (!that.liveCells.Contains(thisCell))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (Cell cell in originalCells)
            {
                hash = hash * 23 + cell.GetHashCode();
            }
            return hash;
        }

    }

}

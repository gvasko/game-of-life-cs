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
        private Cell[] liveCells;
        private BoundingBox boundingBox;

        internal DefaultLifeState(Cell[] liveCells)
        {
            this.liveCells = liveCells;
            boundingBox = new BoundingBox(this.liveCells);
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
                foreach (Cell alive in liveCells)
                {
                    if (alive.Equals(cell))
                    {
                        return CellStatus.Alive;
                    }
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
    }

}

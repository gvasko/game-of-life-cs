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
        private Coord[] liveCells;
        private BoundingBox boundingBox;

        internal DefaultLifeState(Coord[] liveCells)
        {
            this.liveCells = liveCells;
            this.boundingBox = new BoundingBox(this.liveCells);
        }

        public BoundingBox BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }

        public CellStatus GetCellStatus(Coord coord)
        {
            if (BoundingBox.IsInside(coord))
            {
                foreach (Coord alive in liveCells)
                {
                    if (alive.Equals(coord))
                    {
                        return CellStatus.Alive;
                    }
                }
            }
            return CellStatus.Dead;
        }

        public void VisitLiveCells(CellVisitorDelegate visitor)
        {
            foreach (Coord alive in liveCells)
            {
                visitor(alive, CellStatus.Alive);
            }
        }

    }

}

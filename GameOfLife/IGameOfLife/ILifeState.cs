using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    public enum CellStatus { Alive, Dead };

    public struct Coord
    {
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x;
        public int X { get { return x; } }

        private int y;
        public int Y { get { return y; } }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
    }

    public struct BoundingBox
    {
        public BoundingBox(int x1, int y1, int x2, int y2)
        {
            minPoint = new Coord(Math.Min(x1, x2), Math.Min(y1, y2));
            maxPoint = new Coord(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        public BoundingBox(Coord[] coords)
        {
            minPoint = new Coord(coords.Select(c => c.X).Min(), coords.Select(c => c.Y).Min());
            maxPoint = new Coord(coords.Select(c => c.X).Max(), coords.Select(c => c.Y).Max());
        }

        private Coord minPoint;
        public Coord MinPoint { get { return minPoint; } }

        private Coord maxPoint;
        public Coord MaxPoint { get { return maxPoint; } }

        public bool IsInside(Coord coord)
        {
            return
                minPoint.X <= coord.X && coord.X <= maxPoint.X &&
                minPoint.Y <= coord.Y && coord.Y <= maxPoint.Y;
        }

    }

    public delegate void CellVisitorDelegate(Coord coord, CellStatus status);

    public interface ILifeState
    {
        BoundingBox BoundingBox { get; }

        CellStatus GetCellStatus(Coord coord);

        void VisitLiveCells(CellVisitorDelegate visitor);
    }
}

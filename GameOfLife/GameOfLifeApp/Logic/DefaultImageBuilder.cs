using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGameOfLife;
using System.Drawing.Drawing2D;

namespace GameOfLifeApp.Logic
{
    class DefaultImageBuilder : IImageBuilder
    {

        public DefaultImageBuilder()
        {
            BackgroundColor = Color.WhiteSmoke;
            CellBrush = Brushes.SteelBlue;
            GridPen = Pens.Silver;
            CellSize = 1;
        }

        public Color BackgroundColor
        {
            get; set;
        }

        public Brush CellBrush
        {
            get; set;
        }

        public Pen GridPen
        {
            get; set;
        }

        public bool GridEnabled
        {
            get; set;
        }

        public int CellSize
        {
            get; set;
        }

        public Image AsImage(ILifeState state)
        {
            Bitmap image = CreateNewBitmap(state.BoundingBox);

            GraphicsPath cells = CreateCells(state);
            GraphicsPath grid = CreateGrid(state.BoundingBox);

            Matrix tr = CreateDefaultTransform(state.BoundingBox);
            cells.Transform(tr);
            grid.Transform(tr);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(BackgroundColor);
                g.FillPath(CellBrush, cells);
                g.DrawPath(GridPen, grid);
            }

            return image;
        }

        private Bitmap CreateNewBitmap(BoundingBox box)
        {
            int imageWidth = (box.Width + 2) * CellSize;
            int imageHeight = (box.Height + 2) * CellSize;
            return new Bitmap(imageWidth, imageHeight);
        }

        private Matrix CreateDefaultTransform(BoundingBox box)
        {
            Matrix tr = new Matrix();
            tr.Scale(CellSize, CellSize);
            tr.Translate(-(box.MinPoint.X - 1), -(box.MinPoint.Y - 1));
            return tr;
        }

        private static GraphicsPath CreateCells(ILifeState state)
        {
            GraphicsPath cells = new GraphicsPath();

            state.VisitLiveCells((Cell cell, CellStatus status) =>
            {
                cells.AddRectangle(new Rectangle(cell.X, cell.Y, 1, 1));
            });

            return cells;
        }

        private GraphicsPath CreateGrid(BoundingBox box)
        {
            GraphicsPath grid = new GraphicsPath();

            if (!GridEnabled)
            {
                return grid;
            }

            int minX = box.MinPoint.X;
            int minY = box.MinPoint.Y;
            int maxX = box.MaxPoint.X;
            int maxY = box.MaxPoint.Y;

            for (int x = minX; x <= maxX + 1; x++)
            {
                grid.AddLine(x, minY, x, (maxY + 1));
                grid.CloseFigure();
            }

            for (int y = minY; y <= maxY + 1; y++)
            {
                grid.AddLine(minX, y, (maxX + 1), y);
                grid.CloseFigure();
            }

            return grid;
        }

    }
}

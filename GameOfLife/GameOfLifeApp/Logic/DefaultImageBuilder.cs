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
            int imageWidth = (state.BoundingBox.Width + 2) * CellSize;
            int imageHeight = (state.BoundingBox.Height + 2) * CellSize;
            Bitmap image = new Bitmap(imageWidth, imageHeight);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(BackgroundColor);

                int minX = state.BoundingBox.MinPoint.X;
                int minY = state.BoundingBox.MinPoint.Y;
                g.TranslateTransform(-(minX - 1) * CellSize, -(minY - 1) * CellSize);

                state.VisitLiveCells((Cell cell, CellStatus status) =>
                {
                    g.FillRectangle(CellBrush, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
                });

                if (GridEnabled)
                {
                    int maxX = state.BoundingBox.MaxPoint.X;
                    int maxY = state.BoundingBox.MaxPoint.Y;
                    for (int x = state.BoundingBox.MinPoint.X; x <= state.BoundingBox.MaxPoint.X + 1; x++)
                    {
                        g.DrawLine(GridPen, x * CellSize, minY * CellSize, x * CellSize, (maxY + 1) * CellSize);
                    }
                    for (int y = state.BoundingBox.MinPoint.Y; y <= state.BoundingBox.MaxPoint.Y + 1; y++)
                    {
                        g.DrawLine(GridPen, minX * CellSize, y * CellSize, (maxX + 1) * CellSize, y * CellSize);
                    }
                }
            }

            return image;
        }
    }
}

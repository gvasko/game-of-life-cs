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
        public Color BackgroundColor
        {
            get; set;
        }

        public Color CellColor
        {
            get; set;
        }

        public Color GridColor
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
                g.Clear(Color.White);

                g.TranslateTransform(-(state.BoundingBox.MinPoint.X - 1) * CellSize, -(state.BoundingBox.MinPoint.Y - 1) * CellSize);

                state.VisitLiveCells((Cell cell, CellStatus status) =>
                {
                    g.FillRectangle(Brushes.Black, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
                });
            }

            return image;
        }
    }
}

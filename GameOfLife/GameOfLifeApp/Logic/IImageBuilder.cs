using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp.Logic
{
    public interface IImageBuilder
    {
        bool GridEnabled { get; set; }

        Pen GridPen { get; set; }
        Brush CellBrush { get; set; }
        Color BackgroundColor { get; set; }

        int CellSize { get; set; }

        Image AsImage(ILifeState state);
    }
}

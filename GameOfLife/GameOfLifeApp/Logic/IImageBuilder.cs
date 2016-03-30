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

        Color GridColor { get; set; }
        Color CellColor { get; set; }
        Color BackgroundColor { get; set; }

        int CellSize { get; set; }

        Image AsImage(ILifeState state);
    }
}

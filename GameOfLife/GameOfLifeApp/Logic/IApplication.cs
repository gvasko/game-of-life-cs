using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp.Logic
{
    public interface IApplication
    {
        Image Image { get; }

        event EventHandler ImageChanged;

        int CellSize { get; set; }

        event EventHandler CellSizeChanged;

        string File { get; set; }

        event EventHandler FileChanged;

        void ProcessCommandLineArguments(string[] args);

        void NextImage();

        void StartAnimation();

        void StopAnimation();

        void ResetImage();
    }
}

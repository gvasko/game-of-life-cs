using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp.Logic
{
    interface IApplication
    {
        Image Image { get; }

        event EventHandler ImageChanged;

        File File { get; set; }

        event EventHandler FileChanged;

        void ProcessCommandLineArguments(string[] args);

        void NextImage();

        void StartAnimation();

        void StopAnimation();

        void ResetImage();
    }
}

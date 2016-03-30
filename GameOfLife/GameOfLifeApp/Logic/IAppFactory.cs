using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApp.Logic
{
    public interface IAppFactory
    {
        Image CreateLogo();

        IDocument LoadFile(string path);

    }
}

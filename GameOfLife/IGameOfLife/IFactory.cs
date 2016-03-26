using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{
    interface IFactory
    {
        ILifeState LoadLifeStateFromFile(string path);
        ILifeState CreateLifeStateFromCoordinates(int[][] liveCoordinates);
    }
}

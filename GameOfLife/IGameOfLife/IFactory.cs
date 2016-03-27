using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{
    public interface IFactory
    {
        ILifeState LoadLifeStateFromFile(string path);
        ILifeState CreateLifeState(Cell[] cells);

        ILife CreateLife();
    }
}

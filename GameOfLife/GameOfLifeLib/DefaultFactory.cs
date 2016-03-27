using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public sealed class DefaultFactory : IFactory
    {
        private static DefaultFactory factorySoleInstance;

        public static IFactory GetFactory()
        {
            // TODO: not thread safe
            if (factorySoleInstance == null)
            {
                factorySoleInstance = new DefaultFactory();
            }

            return factorySoleInstance;
        }

        public ILife CreateLife()
        {
            return new DefaultLife(this);
        }

        public ILifeState CreateLifeState(Cell[] cells)
        {
            return new DefaultLifeState(cells);
        }

        public ILifeState LoadLifeStateFromFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}

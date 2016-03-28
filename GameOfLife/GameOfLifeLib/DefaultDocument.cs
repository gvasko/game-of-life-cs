using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    internal class DefaultDocument : IDocument
    {
        private IFactory factory;

        public DefaultDocument(IFactory factory)
        {
            this.factory = factory;
        }

        private CellStatus alma()
        {
            return null;
        }

        public ILifeState CurrentState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler CurrentStateChanged;

        public void NextState()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

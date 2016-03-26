using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{
    public interface IDocument
    {
        ILifeState CurrentState { get; }

        event EventHandler CurrentStateChanged;

        void NextState();

        void Reset();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    public delegate bool LifeConditionDelegate(ILifeState state, Cell cell);

    public interface ILife
    {
        void AddRule(LifeConditionDelegate condition, CellStatus consequence);

        ILifeState CalculateNextState(ILifeState currentState);
    }
}

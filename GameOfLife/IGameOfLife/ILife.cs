using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    public delegate CellStatus LifeConditionDelegate(ILifeState state, Cell cell);

    public interface ILife
    {
        void AddRule(LifeConditionDelegate condition);

        ILifeState CalculateNextState(ILifeState currentState);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameOfLife
{

    public delegate bool LifeRuleConditionDelegate(ILifeState state, Cell cell);
    public delegate CellStatus LifeRuleDelegate(ILifeState state, Cell cell);

    public interface ILife
    {
        void AddRule(LifeRuleConditionDelegate condition, LifeRuleDelegate rule);

        ILifeState CalculateNextState(ILifeState currentState);
    }
}

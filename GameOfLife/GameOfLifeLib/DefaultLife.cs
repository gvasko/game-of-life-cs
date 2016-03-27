using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    internal class DefaultLife : ILife
    {
        public void AddRule(LifeRuleConditionDelegate condition, LifeRuleDelegate rule)
        {
            throw new NotImplementedException();
        }

        public ILifeState CalculateNextState(ILifeState currentState)
        {
            throw new NotImplementedException();
        }
    }
}

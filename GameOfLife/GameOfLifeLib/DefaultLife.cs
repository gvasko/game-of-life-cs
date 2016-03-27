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
        private IDictionary<LifeRuleDelegate, LifeRuleConditionDelegate> ruleConditions;
        private IList<LifeRuleDelegate> orderedRules;
        private IFactory factory;
        private ILifeState currentState;
        private IList<Cell> nextLiveCells;

        public DefaultLife(IFactory factory)
        {
            this.factory = factory;
            ruleConditions = new Dictionary<LifeRuleDelegate, LifeRuleConditionDelegate>();
            orderedRules = new List<LifeRuleDelegate>();
            nextLiveCells = null;
        }

        public void AddRule(LifeRuleConditionDelegate condition, LifeRuleDelegate rule)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            if (ruleConditions.ContainsKey(rule))
            {
                return;
            }

            ruleConditions[rule] = condition;
            orderedRules.Add(rule);
        }

        public ILifeState CalculateNextState(ILifeState currentState)
        {
            this.currentState = currentState;
            nextLiveCells = new List<Cell>();

            currentState.VisitEachCell(CalculateNextCellStatus);

            var nextState = factory.CreateLifeState(nextLiveCells.ToArray());

            this.currentState = null;
            nextLiveCells = null;

            return nextState;
        }

        private void CalculateNextCellStatus(Cell cell, CellStatus status)
        {
            bool processed = false;
            foreach (LifeRuleDelegate rule in orderedRules)
            {
                if (ruleConditions[rule](currentState, cell))
                {
                    CellStatus nextStatus = rule(currentState, cell);
                    if (nextStatus == CellStatus.Alive)
                    {
                        nextLiveCells.Add(cell);
                    }
                    processed = true;
                    break;
                }
            }
            if (!processed)
            {
                throw new InvalidOperationException("Undefined cell status");
            }
        }
    }
}

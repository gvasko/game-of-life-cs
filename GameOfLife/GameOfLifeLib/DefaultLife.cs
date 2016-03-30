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
        private IList<LifeRule> rules;
        private IDocFactory factory;
        private ILifeState currentState;
        private IList<Cell> nextLiveCells;

        public DefaultLife(IDocFactory factory)
        {
            this.factory = factory;
            rules = new List<LifeRule>();
            nextLiveCells = null;
        }

        public void AddRule(LifeRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            if (rules.Contains(rule))
            {
                return;
            }

            rules.Add(rule);
        }

        public void AddRules(LifeRule[] rules)
        {
            foreach (LifeRule rule in rules)
            {
                AddRule(rule);
            }
        }

        public ILifeState CalculateNextState(ILifeState currentState)
        {
            if (rules.Count == 0)
            {
                return currentState;
            }

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
            foreach (LifeRule rule in rules)
            {
                CellStatus nextStatus = rule(currentState, cell);

                if (nextStatus == null)
                {
                    continue;
                }

                if (nextStatus == CellStatus.Alive)
                {
                    nextLiveCells.Add(cell);
                }
                processed = true;
                break;
            }
            if (!processed && status == CellStatus.Alive)
            {
                throw new InvalidOperationException("Undefined cell status");
            }
        }

    }
}

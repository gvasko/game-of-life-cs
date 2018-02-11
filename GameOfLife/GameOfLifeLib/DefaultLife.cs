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

        public DefaultLife(IDocFactory factory)
        {
            this.factory = factory;
            rules = new List<LifeRule>();
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

            StateCalculator stateCalculator = new StateCalculator(currentState, rules);

            currentState.VisitEachCell(stateCalculator.CalculateNextCellStatus);

            return factory.CreateLifeState(stateCalculator.NextLiveCells);
        }

        private class StateCalculator
        {
            private IList<LifeRule> rules;
            private ILifeState currentState;
            private IList<Cell> nextLiveCells;

            public Cell[] NextLiveCells
            {
                get { return nextLiveCells.ToArray(); }
            }

            public StateCalculator(ILifeState currentState, IList<LifeRule> rules)
            {
                this.rules = rules;
                this.currentState = currentState;
                this.nextLiveCells = new List<Cell>();
            }

            public void CalculateNextCellStatus(Cell cell, CellStatus status)
            {
                CellStatus nextStatus = null;

                foreach (LifeRule rule in rules)
                {
                    nextStatus = rule(currentState, cell);

                    if (nextStatus == null)
                    {
                        continue;
                    }

                    if (nextStatus == CellStatus.Alive)
                    {
                        nextLiveCells.Add(cell);
                    }
                    break;
                }

                if (status == CellStatus.Alive && nextStatus == null)
                {
                    throw new InvalidOperationException("Undefined cell status");
                }
            }

        }


    }
}

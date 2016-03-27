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
        private IDictionary<LifeConditionDelegate, CellStatus> consequences;
        private IList<LifeConditionDelegate> orderedConditions;
        private IFactory factory;
        private ILifeState currentState;
        private IList<Cell> nextLiveCells;

        public DefaultLife(IFactory factory)
        {
            this.factory = factory;
            consequences = new Dictionary<LifeConditionDelegate, CellStatus>();
            orderedConditions = new List<LifeConditionDelegate>();
            nextLiveCells = null;
        }

        public void AddRule(LifeConditionDelegate condition, CellStatus consequence)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (consequences.ContainsKey(condition))
            {
                return;
            }

            consequences[condition] = consequence;
            orderedConditions.Add(condition);
        }

        public ILifeState CalculateNextState(ILifeState currentState)
        {
            if (orderedConditions.Count == 0)
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
            foreach (LifeConditionDelegate condition in orderedConditions)
            {
                if (condition(currentState, cell))
                {
                    CellStatus nextStatus = consequences[condition];
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

using IGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public sealed class DefaultRules
    {
        const int MinNormalPopulation = 2;
        const int MaxNormalPopulation = 3;

        private DefaultRules()
        {
            throw new InvalidOperationException("Internal error: no instances allowed.");
        }

        public static CellStatus ApplyUnderPopulationRule(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Alive && 
                GetLiveNeighboursCount(state, cell) < MinNormalPopulation)
            {
                return CellStatus.Dead;
            }

            return null;
        }

        public static CellStatus ApplyNormalPopulationRule(ILifeState state, Cell cell)
        {
            int liveNeighboursCount = GetLiveNeighboursCount(state, cell);
            if (state.GetCellStatus(cell) == CellStatus.Alive &&
                MinNormalPopulation <= liveNeighboursCount &&
                liveNeighboursCount <= MaxNormalPopulation)
            {
                return CellStatus.Alive;
            }

            return null;
        }

        public static CellStatus ApplyOverPopulationRule(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Alive &&
                GetLiveNeighboursCount(state, cell) > MaxNormalPopulation)
            {
                return CellStatus.Dead;
            }

            return null;
        }

        public static CellStatus ApplyReproductionRule(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Dead &&
                GetLiveNeighboursCount(state, cell) == MaxNormalPopulation)
            {
                return CellStatus.Alive;
            }

            return null;
        }

        private static int GetLiveNeighboursCount(ILifeState state, Cell cell)
        {
            int liveNeighboursCount = 0;
            state.VisitEachNeightboursOfCell(cell, delegate (Cell c, CellStatus status)
            {
                if (status == CellStatus.Alive)
                {
                    liveNeighboursCount++;
                }
            });

            return liveNeighboursCount;
        }

    }
}

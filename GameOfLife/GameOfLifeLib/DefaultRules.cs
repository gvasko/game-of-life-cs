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

        public static bool CheckIfUnderPopulationHappened(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Dead)
            {
                return false;
            }

            return GetLiveNeighboursCount(state, cell) < MinNormalPopulation;
        }

        public static CellStatus GetResultOfUnderPopulation()
        {
            return CellStatus.Dead;
        }

        public static bool CheckIfNormalPopulationHappened(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Dead)
            {
                return false;
            }

            int liveNeighboursCount = GetLiveNeighboursCount(state, cell);
            return
                MinNormalPopulation <= liveNeighboursCount &&
                liveNeighboursCount <= MaxNormalPopulation;
        }

        public static CellStatus GetResultOfNormalPopulation()
        {
            return CellStatus.Alive;
        }

        public static bool CheckIfOverPopulationHappened(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Dead)
            {
                return false;
            }

            return GetLiveNeighboursCount(state, cell) > MaxNormalPopulation;
        }

        public static CellStatus GetResultOfOverPopulation()
        {
            return CellStatus.Dead;
        }

        public static bool CheckIfReproductionHappened(ILifeState state, Cell cell)
        {
            if (state.GetCellStatus(cell) == CellStatus.Alive)
            {
                return false;
            }

            return GetLiveNeighboursCount(state, cell) == MaxNormalPopulation;
        }

        public static CellStatus GetResultOfReproduction()
        {
            return CellStatus.Alive;
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

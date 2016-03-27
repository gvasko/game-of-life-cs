using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGameOfLife;
using GameOfLifeLib;
using NSubstitute;

namespace GameOfLifeLibTest
{
    [TestClass]
    public class DefaultLifeStateObjTest
    {
        private Cell alive1;
        private Cell alive2;

        [TestInitialize]
        public void Given3x3()
        {
            alive1 = new Cell(-1, 1);
            alive2 = new Cell(1, -1);
        }

        [TestMethod]
        public void GivenSameStates_WhenCompared_ThenEqual()
        {
            DefaultLifeState state1 = new DefaultLifeState(new Cell[] { alive1, alive2 });
            DefaultLifeState state2 = new DefaultLifeState(new Cell[] { alive2, alive1 });
            Assert.AreEqual(state1, state2);
        }

        [TestMethod]
        public void GivenSameStates_WhenCompared_ThenHashCodesEqual()
        {
            DefaultLifeState state1 = new DefaultLifeState(new Cell[] { alive1, alive2 });
            DefaultLifeState state2 = new DefaultLifeState(new Cell[] { alive2, alive1 });
            Assert.AreEqual(state1.GetHashCode(), state2.GetHashCode());
        }

        [TestMethod]
        public void GivenDifferentStates_WhenCompared_ThenNotEqual()
        {
            DefaultLifeState state1 = new DefaultLifeState(new Cell[] { alive1, alive2 });
            DefaultLifeState state2 = new DefaultLifeState(new Cell[] { alive1 });
            Assert.AreNotEqual(state1, state2);
        }

        [TestMethod]
        public void GivenSameCells_WhenCreating_ThenDuplicationsRemoved()
        {
            DefaultLifeState state = new DefaultLifeState(new Cell[] { alive1, alive2, alive1 });
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();

            state.VisitLiveCells(spyVisitor);

            spyVisitor.Received(1).Invoke(Arg.Is<Cell>(alive1), Arg.Any<CellStatus>());
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGameOfLife;
using GameOfLifeLib;
using NSubstitute;

namespace GameOfLifeLibTest
{
    [TestClass]
    public class DefaultLifeStateTest
    {
        private Coord alive1;
        private Coord alive2;
        private Coord dead1;
        private Coord dead2;
        private DefaultLifeState state;

        [TestInitialize]
        public void Given3x3()
        {
            alive1 = new Coord(-1, 1);
            alive2 = new Coord(1, -1);
            dead1 = new Coord(-1, -1);
            dead2 = new Coord(1, 1);
            Coord[] testData = { alive1, alive2 };
            state = new DefaultLifeState(testData);
        }

        [TestMethod]
        public void Given3x3_WhenCreated_ThenBoundingBox3x3()
        {
            Assert.AreEqual(dead1, state.BoundingBox.MinPoint);
            Assert.AreEqual(dead2, state.BoundingBox.MaxPoint);
        }

        [TestMethod]
        public void Given3x3_WhenCreated_ThenOnly2Alive()
        {
            Assert.AreEqual(CellStatus.Alive, state.GetCellStatus(alive1));
            Assert.AreEqual(CellStatus.Alive, state.GetCellStatus(alive2));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(dead1));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(dead2));
        }

        [TestMethod]
        public void Given3x3_WhenCreated_ThenAroundAllDead()
        {
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(0, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(1, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(2, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(2, 1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(2, 0)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(2, -1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(2, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(1, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(0, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-1, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-2, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-2, -1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-2, 0)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-2, 1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-2, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-1, 2)));
        }

        [TestMethod]
        public void Given3x3_WhenCreated_ThenFarAwayAllDead()
        {
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(1000, 2000)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Coord(-1000, -2000)));
        }

        [TestMethod]
        public void Given3x3_WhenVisited_ThenOriginalOrder()
        {
            var spyVisitor = Substitute.For<CellVisitorDelegate>();
            state.VisitLiveCells(spyVisitor);
            Received.InOrder(() =>
            {
                spyVisitor.Received().Invoke(Arg.Is<Coord>(alive1), Arg.Any<CellStatus>());
                spyVisitor.Received().Invoke(Arg.Is<Coord>(alive2), Arg.Any<CellStatus>());
                spyVisitor.DidNotReceive();
            });
        }

        //[TestMethod]
        //public void GivenTwoInitialStates_WhenShapesJustMoved_ThenTheyEqual()
        //{
        //    // This test saved me, because it revealed the misunderstanding in my premature model
        //}

    }
}

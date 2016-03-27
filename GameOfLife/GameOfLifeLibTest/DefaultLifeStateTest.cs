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
        private Cell alive1;
        private Cell alive2;
        private Cell dead1;
        private Cell dead2;
        private DefaultLifeState state;

        [TestInitialize]
        public void Given3x3()
        {
            alive1 = new Cell(-1, 1);
            alive2 = new Cell(1, -1);
            dead1 = new Cell(-1, -1);
            dead2 = new Cell(1, 1);
            Cell[] testData = { alive1, alive2 };
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
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(0, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(1, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(2, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(2, 1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(2, 0)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(2, -1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(2, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(1, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(0, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-1, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-2, -2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-2, -1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-2, 0)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-2, 1)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-2, 2)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-1, 2)));
        }

        [TestMethod]
        public void Given3x3_WhenCreated_ThenFarAwayAllDead()
        {
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(1000, 2000)));
            Assert.AreEqual(CellStatus.Dead, state.GetCellStatus(new Cell(-1000, -2000)));
        }

        [TestMethod]
        public void Given3x3_WhenVisitedLiveCells_ThenOriginalOrder()
        {
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();
            state.VisitLiveCells(spyVisitor);

            Received.InOrder(() =>
            {
                spyVisitor.Received().Invoke(Arg.Is<Cell>(alive1), Arg.Any<CellStatus>());
                spyVisitor.Received().Invoke(Arg.Is<Cell>(alive2), Arg.Any<CellStatus>());
                spyVisitor.DidNotReceive();
            });
        }

        [TestMethod]
        public void Given3x3_WhenVisitedEachCell_ThenOrderIsBasedOnCoordinates()
        {
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();
            state.VisitEachCell(spyVisitor);

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, -1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, -1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, -1)), Arg.Is<CellStatus>(CellStatus.Alive));

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, 1)), Arg.Is<CellStatus>(CellStatus.Alive));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 1)), Arg.Is<CellStatus>(CellStatus.Dead));

            spyVisitor.DidNotReceive();
        }

        [TestMethod]
        public void Given3x3_WhenVisitedEachNeighbourInTheMiddle_ThenGives8ofThem()
        {
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();
            state.VisitEachNeightboursOfCell(new Cell(0, 0), spyVisitor);

            spyVisitor.Received(8).Invoke(Arg.Any<Cell>(), Arg.Any<CellStatus>());

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, -1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, -1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, -1)), Arg.Is<CellStatus>(CellStatus.Alive));

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(0, 0)), Arg.Any<CellStatus>());
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, 1)), Arg.Is<CellStatus>(CellStatus.Alive));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 1)), Arg.Is<CellStatus>(CellStatus.Dead));
        }

        [TestMethod]
        public void Given3x3_WhenVisitedEachNeighbourOnTheCorner_ThenGives8ofThem()
        {
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();
            state.VisitEachNeightboursOfCell(new Cell(1, 1), spyVisitor);

            spyVisitor.Received(8).Invoke(Arg.Any<Cell>(), Arg.Any<CellStatus>());

            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(-1, -1)), Arg.Any<CellStatus>());
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(0, -1)), Arg.Any<CellStatus>());
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(1, -1)), Arg.Any<CellStatus>());

            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(-1, 0)), Arg.Any<CellStatus>());
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));

            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(-1, 1)), Arg.Any<CellStatus>());
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(1, 1)), Arg.Any<CellStatus>());

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 2)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 2)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(2, 2)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(2, 1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(2, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
        }

        [TestMethod]
        public void Given3x3_WhenVisitedEachNeighbourOnTheSide_ThenGives8ofThem()
        {
            var spyVisitor = Substitute.For<CellStatusVisitorDelegate>();
            state.VisitEachNeightboursOfCell(new Cell(0, -1), spyVisitor);

            spyVisitor.Received(8).Invoke(Arg.Any<Cell>(), Arg.Any<CellStatus>());

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, -1)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(0, -1)), Arg.Any<CellStatus>());
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, -1)), Arg.Is<CellStatus>(CellStatus.Alive));

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, 0)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, 0)), Arg.Is<CellStatus>(CellStatus.Dead));

            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(-1, 1)), Arg.Any<CellStatus>());
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(0, 1)), Arg.Any<CellStatus>());
            spyVisitor.DidNotReceive().Invoke(Arg.Is<Cell>(new Cell(1, 1)), Arg.Any<CellStatus>());

            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(-1, -2)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(0, -2)), Arg.Is<CellStatus>(CellStatus.Dead));
            spyVisitor.Received().Invoke(Arg.Is<Cell>(new Cell(1, -2)), Arg.Is<CellStatus>(CellStatus.Dead));
        }

        [TestMethod]
        public void Given3x3_WhenComparedWithSameState_ThenEquals()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Given3x3_WhenSameStates_ThenHashCodesAreEqual()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Given3x3_WhenComparedWithDifferentState_ThenDoesNotEqual()
        {
            throw new NotImplementedException();
        }

        //[TestMethod]
        //public void GivenTwoInitialStates_WhenShapesJustMoved_ThenTheyEqual()
        //{
        //    // This test saved me, because it revealed the misunderstanding in my premature model
        //}

    }
}

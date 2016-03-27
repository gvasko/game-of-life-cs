using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGameOfLife;
using NSubstitute;
using GameOfLifeLib;

namespace GameOfLifeLibTest
{
    [TestClass]
    public class DefaultLifeTest
    {
        private DefaultLife life;
        private ILifeState dummyState;
        private ILifeState mockLiveState;
        private ILifeState mockDeadState;
        private IFactory spyFactory;

        [TestInitialize]
        public void GivenDummyState()
        {
            spyFactory = Substitute.For<IFactory>();
            life = new DefaultLife(spyFactory);
            dummyState = Substitute.For<ILifeState>();

            var dummyCell = new Cell(0, 0);

            mockLiveState = Substitute.For<ILifeState>();

            mockLiveState
                .WhenForAnyArgs(s => s.VisitEachCell(Arg.Any<CellStatusVisitorDelegate>()))
                .Do(s => s.Arg<CellStatusVisitorDelegate>()(dummyCell, CellStatus.Alive));

            mockDeadState = Substitute.For<ILifeState>();

            mockDeadState
                .WhenForAnyArgs(s => s.VisitEachCell(Arg.Any<CellStatusVisitorDelegate>()))
                .Do(s => s.Arg<CellStatusVisitorDelegate>()(dummyCell, CellStatus.Dead));

        }

        [TestMethod]
        public void GivenDummyStateAndNoRules_WhenGetNext_ThenReturnsOriginal()
        {
            ILifeState nextState = life.CalculateNextState(dummyState);
            Assert.AreSame(dummyState, nextState);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GivenMockStateAndRules_WhenNothingIsApplicableOnALiveCell_ThenThrowsException()
        {
            var fakeCondition = CreateAlwaysFalseCondition();
            var fakeConsequence = CellStatus.Alive;

            life.AddRule(fakeCondition, fakeConsequence);

            life.CalculateNextState(mockLiveState);
        }

        [TestMethod]
        public void GivenDeadStateAndRules_WhenNothingIsApplicable_ThenNothingHappens()
        {
            var spyCondition1 = CreateAlwaysFalseCondition();
            var spyCondition2 = CreateAlwaysFalseCondition();
            var fakeConsequence = CellStatus.Dead;
            int liveCellCount = 0;

            life.AddRule(spyCondition1, fakeConsequence);
            life.AddRule(spyCondition2, fakeConsequence);

            life.CalculateNextState(mockDeadState);

            spyCondition1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyCondition2.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyFactory.Received(1).CreateLifeState(Arg.Is<Cell[]>(cellArray => cellArray.Length == liveCellCount));
        }

        [TestMethod]
        public void GivenDeadStateAndRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            var spyCondition1 = CreateAlwaysTrueCondition();
            var spyCondition2 = CreateAlwaysTrueCondition();
            var fakeConsequence = CellStatus.Dead;
            int liveCellCount = 0;

            life.AddRule(spyCondition1, fakeConsequence);
            life.AddRule(spyCondition2, fakeConsequence);

            life.CalculateNextState(mockDeadState);

            spyCondition1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyCondition2.DidNotReceive().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyFactory.Received(1).CreateLifeState(Arg.Is<Cell[]>(cellArray => cellArray.Length == liveCellCount));
        }

        private static LifeConditionDelegate CreateAlwaysTrueCondition()
        {
            var alwaysTrue = Substitute.For<LifeConditionDelegate>();
            alwaysTrue.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(true);
            return alwaysTrue;
        }

        private static LifeConditionDelegate CreateAlwaysFalseCondition()
        {
            var alwaysFalse = Substitute.For<LifeConditionDelegate>();
            alwaysFalse.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(false);
            return alwaysFalse;
        }

    }
}

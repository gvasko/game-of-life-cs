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
        private ILifeState mockState;
        private IFactory spyFactory;

        [TestInitialize]
        public void GivenDummyState()
        {
            spyFactory = Substitute.For<IFactory>();
            life = new DefaultLife(spyFactory);
            dummyState = Substitute.For<ILifeState>();

            mockState = Substitute.For<ILifeState>();
            var dummyCell = new Cell(0, 0);
            var dummyCellState = CellStatus.Alive;

            mockState
                .WhenForAnyArgs(s => s.VisitEachCell(Arg.Any<CellStatusVisitorDelegate>()))
                .Do(s => s.Arg<CellStatusVisitorDelegate>()(dummyCell, dummyCellState));

        }

        [TestMethod]
        public void GivenDummyStateAndNoRules_WhenGetNext_ThenReturnsOriginal()
        {
            ILifeState nextState = life.CalculateNextState(dummyState);
            Assert.AreSame(dummyState, nextState);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GivenMockStateAndRules_WhenNothingIsApplicable_ThenThrowsException()
        {
            var alwaysFalse = Substitute.For<LifeConditionDelegate>();
            alwaysFalse.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(false);
            var dummyConsequence = CellStatus.Alive;

            life.AddRule(alwaysFalse, dummyConsequence);

            life.CalculateNextState(mockState);
        }

        [TestMethod]
        public void GivenMockStateAndRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            LifeConditionDelegate spyCondition1 = CreateAlwaysTrueCondition();
            LifeConditionDelegate spyCondition2 = CreateAlwaysTrueCondition();

            var dummyConsequence = CellStatus.Alive;

            life.AddRule(spyCondition1, dummyConsequence);
            life.AddRule(spyCondition2, dummyConsequence);

            life.CalculateNextState(mockState);

            spyCondition1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyCondition2.DidNotReceive().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
        }

        private static LifeConditionDelegate CreateAlwaysTrueCondition()
        {
            var alwaysTrue = Substitute.For<LifeConditionDelegate>();
            alwaysTrue.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(true);
            return alwaysTrue;
        }
    }
}

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
            var alwaysFalse = Substitute.For<LifeRuleConditionDelegate>();
            alwaysFalse.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(false);
            var dummyRule = Substitute.For<LifeRuleDelegate>();

            life.AddRule(alwaysFalse, dummyRule);

            life.CalculateNextState(mockState);
        }

        [TestMethod]
        public void GivenMockStateAndRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            var alwaysTrue = Substitute.For<LifeRuleConditionDelegate>();
            alwaysTrue.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(true);
            var spyRule1 = Substitute.For<LifeRuleDelegate>();
            var spyRule2 = Substitute.For<LifeRuleDelegate>();

            life.AddRule(alwaysTrue, spyRule1);
            life.AddRule(alwaysTrue, spyRule2);

            life.CalculateNextState(mockState);

            spyRule1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyRule2.DidNotReceive().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
        }
    }
}

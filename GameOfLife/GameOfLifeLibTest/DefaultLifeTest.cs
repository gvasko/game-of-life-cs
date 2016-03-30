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
        private DefaultLife sutLife;
        private ILifeState dummyState;
        private ILifeState stubLiveState;
        private ILifeState stubDeadState;
        private IDocFactory spyFactory;

        [TestInitialize]
        public void GivenDummyState()
        {
            spyFactory = Substitute.For<IDocFactory>();
            sutLife = new DefaultLife(spyFactory);
            dummyState = Substitute.For<ILifeState>();

            var dummyCell = new Cell(0, 0);

            stubLiveState = Substitute.For<ILifeState>();

            stubLiveState
                .WhenForAnyArgs(s => s.VisitEachCell(Arg.Any<CellStatusVisitorDelegate>()))
                .Do(s => s.Arg<CellStatusVisitorDelegate>()(dummyCell, CellStatus.Alive));

            stubDeadState = Substitute.For<ILifeState>();

            stubDeadState
                .WhenForAnyArgs(s => s.VisitEachCell(Arg.Any<CellStatusVisitorDelegate>()))
                .Do(s => s.Arg<CellStatusVisitorDelegate>()(dummyCell, CellStatus.Dead));

        }

        [TestMethod]
        public void GivenDummyStateAndNoRules_WhenGetNext_ThenReturnsOriginal()
        {
            ILifeState nextState = sutLife.CalculateNextState(dummyState);
            Assert.AreSame(dummyState, nextState);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GivenMockStateAndRules_WhenNothingIsApplicableOnALiveCell_ThenThrowsException()
        {
            var stubRule = CreateRuleThatNeverApplicable();

            sutLife.AddRule(stubRule);

            sutLife.CalculateNextState(stubLiveState);
        }

        [TestMethod]
        public void GivenDeadStateAndRules_WhenNothingIsApplicable_ThenNothingHappens()
        {
            var spyRule1 = CreateRuleThatNeverApplicable();
            var spyRule2 = CreateRuleThatNeverApplicable();
            int liveCellCount = 0;

            sutLife.AddRule(spyRule1);
            sutLife.AddRule(spyRule2);

            sutLife.CalculateNextState(stubDeadState);

            spyRule1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyRule2.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyFactory.Received(1).CreateLifeState(Arg.Is<Cell[]>(cellArray => cellArray.Length == liveCellCount));
        }

        [TestMethod]
        public void GivenDeadStateAndRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            var spyRule1 = CreateRuleWithConstantStatus(CellStatus.Dead);
            var spyRule2 = CreateRuleWithConstantStatus(CellStatus.Dead);
            int liveCellCount = 0;

            sutLife.AddRule(spyRule1);
            sutLife.AddRule(spyRule2);

            sutLife.CalculateNextState(stubDeadState);

            spyRule1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyRule2.DidNotReceive().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyFactory.Received(1).CreateLifeState(Arg.Is<Cell[]>(cellArray => cellArray.Length == liveCellCount));
        }

        private static LifeRule CreateRuleWithConstantStatus(CellStatus result)
        {
            var alwaysTrue = Substitute.For<LifeRule>();
            alwaysTrue.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(result);
            return alwaysTrue;
        }

        private static LifeRule CreateRuleThatNeverApplicable()
        {
            var alwaysFalse = Substitute.For<LifeRule>();
            CellStatus noResult = null;
            alwaysFalse.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(noResult);
            return alwaysFalse;
        }

    }
}

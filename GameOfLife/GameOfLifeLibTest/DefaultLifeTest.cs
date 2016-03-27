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
        private IFactory spyFactory;

        [TestInitialize]
        public void GivenDummyState()
        {
            spyFactory = Substitute.For<IFactory>();
            life = new DefaultLife(spyFactory);
            dummyState = Substitute.For<ILifeState>();
        }

        [TestMethod]
        public void GivenDummyStateAndNoRules_WhenGetNext_ThenReturnsOriginal()
        {
            ILifeState nextState = life.CalculateNextState(dummyState);
            spyFactory.CreateLifeState(Arg.Any<Cell[]>()).Received(1);
            throw new NotImplementedException("TODO: ensure it copies that");
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GivenDummyStateAndRules_WhenNothingIsApplicable_ThenThrowsException()
        {
            var alwaysFalse = Substitute.For<LifeRuleConditionDelegate>();
            alwaysFalse.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(false);
            var dummyRule = Substitute.For<LifeRuleDelegate>();
            life.AddRule(alwaysFalse, dummyRule);
            life.CalculateNextState(dummyState);
        }

        [TestMethod]
        public void GivenDummyStateAndRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            var alwaysTrue = Substitute.For<LifeRuleConditionDelegate>();
            alwaysTrue.Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>()).Returns(true);
            var spyRule1 = Substitute.For<LifeRuleDelegate>();
            var spyRule2 = Substitute.For<LifeRuleDelegate>();
            life.AddRule(alwaysTrue, spyRule1);
            life.AddRule(alwaysTrue, spyRule2);

            life.CalculateNextState(dummyState);

            // ERROR: dummyState will not call the visitors, find another way
            spyRule1.Received().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
            spyRule2.DidNotReceive().Invoke(Arg.Any<ILifeState>(), Arg.Any<Cell>());
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGameOfLife;
using GameOfLifeLib;

namespace GameOfLifeLibTest
{
    [TestClass]
    public class DefaultRulesIntegrationTest
    {
        private ILife life;

        [TestInitialize]
        public void GivenLifeWithDefaultRules()
        {
            life = DefaultFactory.GetFactory().CreateLife();
            life.AddRule(DefaultRules.ApplyUnderPopulationCondition);
            life.AddRule(DefaultRules.ApplyNormalPopulationCondition);
            life.AddRule(DefaultRules.ApplyOverPopulationCondition);
            life.AddRule(DefaultRules.ApplyReproductionCondition);
        }

        [TestMethod]
        public void Test_Pattern_1_2_3()
        {
            // http://www.conwaylife.com/wiki/1-2-3
            int[,] testData1 = new int[,] { { -3, -4 }, { -2, -4 }, { -5, -3 }, { -2, -3 },
                { -5, -2 }, { -4, -2 }, { -2, -2 }, { 0, -2 }, { 1, -2 }, { -4, -1 }, { -2, -1 },
                { 1, -1 }, { -4, 0 }, { 1, 0 }, { 3, 0 }, { 4, 0 }, { -3, 1 }, { -2, 1 }, { -1, 1 },
                { 1, 1 }, { 3, 1 }, { 4, 1 }, { 0, 2 }, { -1, 3 }, { -1, 4 }, { 0, 4 } };

            ILifeState initialState = DefaultFactory.GetFactory().CreateLifeState(Cell.AsCellArray(testData1));

            int[,] testData2 = new int[,] { { -3, -4 }, { -2, -4 }, { -5, -3 }, { -2, -3 },
                { -5, -2 }, { -4, -2 }, { -2, -2 }, { 0, -2 }, { 1, -2 }, { -4, -1 }, { -1, -1 },
                { 1, -1 }, { -4, 0 }, { -1, 0 }, { 1, 0 }, { 3, 0 }, { 4, 0 }, { -3, 1 }, { -2, 1 }, { -1, 1 },
                { 1, 1 }, { 3, 1 }, { 4, 1 }, { 0, 2 }, { -1, 3 }, { -1, 4 }, { 0, 4 } };

            ILifeState expectedSecondState = DefaultFactory.GetFactory().CreateLifeState(Cell.AsCellArray(testData2));

            ILifeState calculatedSecondState = life.CalculateNextState(initialState);

            Assert.AreEqual(expectedSecondState, calculatedSecondState);

            int[,] testData3 = new int[,] { { -3, -4 }, { -2, -4 }, { -5, -3 }, { -2, -3 },
                { -5, -2 }, { -4, -2 }, { -2, -2 }, { 0, -2 }, { 1, -2 }, { -4, -1 }, { -2, -1 }, { -1, -1 },
                { 1, -1 }, { -4, 0 }, { -1, 0 }, { 1, 0 }, { 3, 0 }, { 4, 0 }, { -3, 1 }, { -2, 1 }, { -1, 1 },
                { 1, 1 }, { 3, 1 }, { 4, 1 }, { 0, 2 }, { -1, 3 }, { -1, 4 }, { 0, 4 } };

            ILifeState expectedThirdState = DefaultFactory.GetFactory().CreateLifeState(Cell.AsCellArray(testData3));

            ILifeState calculatedThirdState = life.CalculateNextState(expectedSecondState);

            Assert.AreEqual(expectedThirdState, calculatedThirdState);

            ILifeState calculatedFourthState = life.CalculateNextState(expectedThirdState);

            Assert.AreEqual(initialState, calculatedFourthState);

        }
    }
}

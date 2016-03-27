using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibTest
{
    [TestClass]
    public class DefaultLifeTest
    {
        [TestInitialize]
        public void Given3x3()
        {

        }

        [TestMethod]
        public void GivenNoRules_WhenGetNext_ThenReturnsOriginal()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GivenRules_WhenNothingIsApplicable_ThenThrowsException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GivenRules_WhenAllAreApplicable_ThenFirstAppliedOnly()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGameOfLife;
using GameOfLifeLib;
using NSubstitute;

namespace GameOfLifeLibTest
{

    [TestClass]
    public class DefaultDocumentTest
    {
        private DefaultDocument sutDocument;
        private ILifeState dummyInitialLifeState;
        private ILifeState dummySecondLifeState;

        [TestInitialize]
        public void GivenASimpleDocument()
        {
            dummyInitialLifeState = Substitute.For<ILifeState>();
            dummySecondLifeState = Substitute.For<ILifeState>();
            Assert.AreNotEqual(dummyInitialLifeState, dummySecondLifeState);

            var stubLife = Substitute.For<ILife>();
            stubLife.CalculateNextState(Arg.Is<ILifeState>(dummyInitialLifeState)).Returns(dummySecondLifeState);

            var stubFactory = Substitute.For<IDocFactory>();
            stubFactory.CreateLife().Returns(stubLife);

            sutDocument = new DefaultDocument(stubFactory, dummyInitialLifeState);
        }

        [TestMethod]
        public void GivenASimpleDocument_WhenGetCurrentState_ThenReturnsTheProvidedState()
        {
            Assert.AreSame(dummyInitialLifeState, sutDocument.CurrentState);
        }

        [TestMethod]
        public void GivenASimpleDocument_WhenNextState_ThenCurrentStateUpdated()
        {
            sutDocument.NextState();
            Assert.AreSame(dummySecondLifeState, sutDocument.CurrentState);
        }

        [TestMethod]
        public void GivenASimpleDocument_WhenNextState_ThenNotifies()
        {
            var spyEventHandler = Substitute.For<EventHandler>();
            sutDocument.CurrentStateChanged += spyEventHandler;
            sutDocument.NextState();
            spyEventHandler.Received(1).Invoke(Arg.Is<object>(sutDocument), Arg.Any<EventArgs>());
            spyEventHandler.ClearReceivedCalls();
            sutDocument.Reset();
            spyEventHandler.Received(1).Invoke(Arg.Is<object>(sutDocument), Arg.Any<EventArgs>());
        }

        [TestMethod]
        public void GivenASimpleDocument_WhenReset_ThenGetsBackToTheInitialState()
        {
            sutDocument.NextState();
            sutDocument.Reset();
            Assert.AreSame(dummyInitialLifeState, sutDocument.CurrentState);
        }

    }
}

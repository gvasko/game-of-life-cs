using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLifeApp.Logic;
using NSubstitute;
using IGameOfLife;
using System.IO;
using System.Drawing;

namespace GameOfLifeAppTest.Logic
{
    [TestClass]
    public class DefaultApplicationTest
    {
        private DefaultApplication sutApp;
        private IAppFactory spyFactory;
        private string dummyExistingFile;
        private string dummyNotExistingFile;
        private IDocument spyDocument;
        private Image dummyLogo;
        private EventHandler spyImageEventHandler;

        [TestInitialize]
        public void GivenNewApp()
        {
            spyDocument = Substitute.For<IDocument>();

            dummyExistingFile = "existing file";
            dummyNotExistingFile = "not existing file";
            dummyLogo = Substitute.For<Image>();

            spyFactory = Substitute.For<IAppFactory>();
            spyFactory.LoadFile(Arg.Is<string>(dummyExistingFile)).Returns(spyDocument);
            spyFactory.When(f => f.LoadFile(Arg.Is<string>(dummyNotExistingFile))).Do(f => { throw new FileNotFoundException(); });
            spyFactory.CreateLogo().Returns(dummyLogo);

            sutApp = new DefaultApplication(spyFactory);

            spyImageEventHandler = Substitute.For<EventHandler>();
            sutApp.ImageChanged += spyImageEventHandler;
        }

        [TestMethod]
        public void GivenNewApp_WhenGetImage_ThenReturnLogo()
        {
            Assert.AreSame(dummyLogo, sutApp.Image);
        }

        [TestMethod]
        public void GivenNewApp_WhenFileSet_ThenImageUpdated()
        {
            var initialImage = sutApp.Image;

            sutApp.File = dummyExistingFile;

            spyImageEventHandler.Received(1).Invoke(Arg.Is<object>(sutApp), Arg.Any<EventArgs>());

            Assert.AreNotSame(initialImage, sutApp.Image);

        }

        [TestMethod]
        public void GivenNewApp_WhenFileNotFound_ThenImageRemainAndThrowsException()
        {
            var initialImage = sutApp.Image;

            try
            {
                sutApp.File = dummyNotExistingFile;
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(FileNotFoundException), e.GetType());
            }

            spyImageEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<EventArgs>());

            Assert.AreSame(initialImage, sutApp.Image);
        }

        [TestMethod, Ignore]
        public void GivenNewApp_WhenCommandLineArgument_ThenLoadsFile()
        {
        }

        [TestMethod, Ignore]
        public void GivenNewApp_WhenCommandLineArgumentNotFound_ThenLogoRemainsAndThrowException()
        {
        }

        [TestMethod]
        public void GivenNewApp_WhenFileLoadedAndNextImage_ThenImageUpdated()
        {
            var initialImage = sutApp.Image;

            sutApp.File = dummyExistingFile;

            spyImageEventHandler.Received(1).Invoke(Arg.Is<object>(sutApp), Arg.Any<EventArgs>());

            Assert.AreNotSame(initialImage, sutApp.Image);
        }

        [TestMethod]
        public void GivenNewApp_WhenFileLoadedAndReset_ThenImageUpdated()
        {
            var initialImage = sutApp.Image;

            sutApp.File = dummyExistingFile;
            sutApp.ResetImage();

            spyImageEventHandler.Received(2).Invoke(Arg.Is<object>(sutApp), Arg.Any<EventArgs>());

            Assert.AreSame(initialImage, sutApp.Image);
        }

        [TestMethod]
        public void GivenNewApp_WhenNextImage_ThenLogoRemains()
        {
            sutApp.NextImage();
            Assert.AreSame(dummyLogo, sutApp.Image);
        }

        [TestMethod]
        public void GivenNewApp_WhenReset_ThenLogoRemains()
        {
            sutApp.ResetImage();
            Assert.AreSame(dummyLogo, sutApp.Image);
        }

        [TestMethod, Ignore]
        public void GivenNewApp_WhenFileLoaded_ThenStartStopAnimation()
        {
            throw new NotImplementedException();
        }

    }
}

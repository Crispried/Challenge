using Challange.Domain.Entities;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class GameInformationTest : TestCase
    {
        private IApplicationController controller;
        private GameInformationPresenter presenter;
        private IGameInformationView view;
        private GameInformation argument;
        private IFileService fileService;
        private IXmlWorker fileWorker;
        private IPathFormatter pathFormatter;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IGameInformationView>();
            fileService = Substitute.For<IFileService>();
            fileWorker = Substitute.For<IXmlWorker>();
            pathFormatter = Substitute.For<IPathFormatter>();
            presenter = new GameInformationPresenter(controller, view,
                                    fileService, fileWorker, pathFormatter);
            argument = Substitute.For<GameInformation>();
            presenter.Run(argument);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.Received().Show();
        }

        [Test]
        public void PrepareApplication()
        {
            // Arrange
            string directoryName = "blabla";
            // Act
            presenter.PrepareApplication(argument);
            // Assert
            argument.Received().SetGameInformation(argument);
            argument.Received().DirectoryName = directoryName;
            fileService.ReceivedWithAnyArgs().CreateDirectory(directoryName);
            var path = pathFormatter.ReceivedWithAnyArgs().FormatPathToGameInformationFile(directoryName);
            fileWorker.ReceivedWithAnyArgs().SerializeXml(argument, path);
            view.Received().Close();
        }
    }
}

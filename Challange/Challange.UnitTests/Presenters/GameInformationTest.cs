using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IFileWorker fileWorker;
        private IPathFormatter pathFormatter;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IGameInformationView>();
            fileService = Substitute.For<IFileService>();
            fileWorker = Substitute.For<IFileWorker>();
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
            // Act
            // Assert
            presenter.PrepareApplication(argument);
            argument.Received().SetGameInformation(argument);
            argument.Received().DirectoryName = "blabla";
            fileService.ReceivedWithAnyArgs().CreateDirectory("blabla");
            pathFormatter.ReceivedWithAnyArgs().FormatPathToGameInformationFile("");
            fileWorker.ReceivedWithAnyArgs().SerializeXml(argument, "");
            view.Received().Close();
        }
    }
}

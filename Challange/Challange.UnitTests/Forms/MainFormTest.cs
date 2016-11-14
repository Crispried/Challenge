using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Challange.Forms;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views.Layouts;
using System.Windows.Forms;

namespace Challange.UnitTests.Forms
{
    [TestFixture]
    class MainFormTest : TestCase
    {
        private IMainView form;
        private IMainFormLayout layout;
        private ApplicationContext context;

        [SetUp]
        public void SetUp()
        {
            context = new ApplicationContext();
            form = new MainForm(context, layout);
            layout = Substitute.For<IMainFormLayout>();
        }

        [Test]
        public void DrawPlayersTest()
        {
            // Arrange
            int numberOfPlayers = 5;
            PlayerPanelSettings settings = InitializePlayerPanelSettings();

            // Act
            form.DrawPlayers(settings, numberOfPlayers);

            // Assert
            layout.Received().DrawPlayers(settings, numberOfPlayers);
        }
    }
}

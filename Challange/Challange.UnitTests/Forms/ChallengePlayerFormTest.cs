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
    class ChallengePlayerFormTest : TestCase
    {
        private IChallengePlayerView form;
        private IChallengePlayerFormLayout layout;

        [SetUp]
        public void SetUp()
        {
            form = new ChallengePlayerForm(layout);
            layout = Substitute.For<IChallengePlayerFormLayout>();
        }

        [Test]
        public void DrawPlayersTest()
        {
            // Arrange
            int numberOfPlayers = 5;

            // Act
            form.DrawPlayers(numberOfPlayers);

            // Assert
            layout.Received().DrawPlayers(numberOfPlayers);
        }
    }
}

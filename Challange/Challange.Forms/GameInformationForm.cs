using Challange.Domain.Services.Challenge;
using Challange.Presenter.Views;
using System;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class GameInformationForm :
                                Form, IGameInformationView
    {
        public GameInformationForm()
        {
            InitializeComponent();
            startGameSettingsButton.Click += (sender, args)
                => Invoke(SetGameInformation, GetGameInformation());
        }

        public new void Show()
        {
            ShowDialog();
        }

        private GameInformation GetGameInformation()
        {
            var gameInformation = new GameInformation()
            {
                FirstTeam = firstTeamTextBox.Text,
                SecondTeam = secondTeamTextBox.Text,
                Date = datePicker.Value.ToString("dd.MM.yyyy"),
                GameStart = timePicker.Value.ToString("H:mm:ss"),
                Country = countryTextBox.Text,
                City = cityTextBox.Text,
                Part = partTextBox.Text
            };
            return gameInformation;
        }

        public event Action<GameInformation> SetGameInformation;
    }
}

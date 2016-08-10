using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Entities;

namespace Challange.Forms
{
    public partial class GameInformationForm :
                                Form, IGameInformationView
    {
        public GameInformationForm()
        {
            InitializeComponent();
            startGameSettingsButton.Click += (sender, args)
                => Invoke(SetGameInformation);
        }

        public new void Show()
        {
            ShowDialog();
        }

        public GameInformation GameInformation
        {
            get
            {
                return GetGameInformation();
            }
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

        public event Action SetGameInformation;
    }
}

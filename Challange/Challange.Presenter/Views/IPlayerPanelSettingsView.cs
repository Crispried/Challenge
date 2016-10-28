using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;

namespace Challange.Presenter.Views
{
    public interface IPlayerPanelSettingsView : IView
    {
        event Action<PlayerPanelSettings> ChangePlayerPanelSettings;

        PlayerPanelSettings PlayerPanelSettings { get; set; }

        bool ValidateForm();

        void SetPlayerPanelSettings(
                    PlayerPanelSettings playerPanelSettings);

        void ShowMessage(ChallengeMessage message);
    }
}

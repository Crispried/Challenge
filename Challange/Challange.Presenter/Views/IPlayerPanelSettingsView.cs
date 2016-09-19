using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;

namespace Challange.Presenter.Views
{
    public interface IPlayerPanelSettingsView : IView
    {
        event Action ChangePlayerPanelSettings;

        PlayerPanelSettings PlayerPanelSettings { get; set; }

        void SetPlayerPanelSettings(
                    PlayerPanelSettings playerPanelSettings);
    }
}

using Challange.Domain.SettingsService.SettingTypes;
using Challange.Presenter.Base;
using System;

namespace Challange.Presenter.Views
{
    public interface IPlayerPanelSettingsView : IView
    {
        event Action ChangePlayerPanelSettings;

        PlayerPanelSettings PlayerPanelSettings { get; }

        void SetPlayerPanelSettings(
                    PlayerPanelSettings playerPanelSettings);
    }
}

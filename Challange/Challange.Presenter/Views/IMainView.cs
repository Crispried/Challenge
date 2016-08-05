using System;
using Challange.Presenter.Base;
using Challange.Domain.SettingsService.SettingTypes;

namespace Challange.Presenter.Views
{
    public interface IMainView : IView
    {
        event Action OpenPlayerPanelSettings;

        void DrawPlayers(PlayerPanelSettings settings);
    }
}

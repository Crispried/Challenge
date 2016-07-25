using Challange.Domain.SettingsService.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

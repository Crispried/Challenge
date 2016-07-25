using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Presenter.Views.Layouts
{
    public interface IMainFormLayout : ILayout
    {
        void DrawPlayers(PlayerPanelSettings settings, int numberOfPlayers);

        FlowLayoutPanel PlayerPanel { get; set; }

        Form Form { get; set; }

        void BindPlayersToCameras(Queue<string> camerasNames);

        void UpdatePlayersImage(string cameraName, Bitmap frameClone);

        event Action<string, string> PassCamerasNamesToPresenterCallback;

        event Action<string> OpenBroadcastForm;
    }
}

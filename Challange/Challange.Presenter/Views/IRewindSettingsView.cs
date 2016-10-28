using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Views
{
    public interface IRewindSettingsView : IView
    {
        event Action<RewindSettings> ChangeRewindSettings;

        void SetRewindSettings(RewindSettings rewindSettings);

        bool ValidateForm();

        void ShowMessage(ChallengeMessage message);
    }
}

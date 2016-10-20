using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    {
        private RewindSettings rewindSettings;

        private string pathToChallenge;

        public override void Run(string argument)
        {
            pathToChallenge = argument;
            rewindSettings = GetRewindSettings();
            if (rewindSettings == null)
            {
                RewindSettingsAreNull = true;
                ShowMessage(MessageType.RewindSettingsFileParseProblem);
            }
            else
            {
                View.Show();
            }
        }

        public void OpenRewindSettings()
        {
            Controller.Run<RewindSettingsPresenter.RewindSettingsPresenter,
               RewindSettings>(rewindSettings);
        }
    }
}

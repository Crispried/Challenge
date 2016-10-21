using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
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
        #region settings
        /// <summary>
        /// read rewind settings from xml file
        /// this action occures only when we run our form
        /// </summary>
        /// <returns></returns>
        private RewindSettings GetRewindSettings()
        {
            var rewindSettingService =
                 new SettingsService<RewindSettings>(
                                new RewindSettingsParser());
            return rewindSettingService.
                        GetSetting();
        }
        #endregion

        private void ShowMessage(MessageType type)
        {
            ChallengeMessage message = MessageParser.GetMessage(type);
            View.ShowMessage(message);
        }
    }
}

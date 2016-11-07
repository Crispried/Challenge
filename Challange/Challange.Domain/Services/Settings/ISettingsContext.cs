using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings
{
    public interface ISettingsContext
    {
        PlayerPanelSettings GetPlayerPanelSetting();

        void SavePlayerPanelSetting(PlayerPanelSettings playerPanelSetting);

        ChallengeSettings GetChallengeSetting();

        void SaveChallengeSetting(ChallengeSettings challengeSetting);

        FtpSettings GetFtpSetting();

        void SaveFtpSetting(FtpSettings ftpSetting);

        RewindSettings GetRewindSetting();

        void SaveRewindSetting(RewindSettings rewindSetting);
    }
}

using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings
{
    public interface INullSettingsContainer
    {
        List<Type> NullSettingTypes { get; }

        bool IsEmpty();

        void CheckPlayerPanelSettingOnNull(PlayerPanelSettings playerPanelSetting);

        void CheckChallengeSettingOnNull(ChallengeSettings challengeSettings);

        void CheckFtpSettingOnNull(FtpSettings ftpSettings);

        void CheckRewindSettingOnNull(RewindSettings rewindSettings);
    }
}

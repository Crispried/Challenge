using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Message
{
    public enum MessageType
    {
        SettingsFilesParseProblem,
        EmptyDeviceContainer,
        HaveNotRecordedAnyChallengeYet,
        TestFtpConnectionSuccessed,
        TestFtpConnectionFailed,
        FtpSettingsInvalid,
        PlayerPanelSettingsInvalid,
        ChallengeSettingsInvalid,
        RewindSettingsInvalid
    };
}

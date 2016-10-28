using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Message
{
    public enum MessageType
    {
        ChallengeSettingsFileParseProblem,
        PlayerPanelSettingsFileParseProblem,
        FtpSettingsFileParseProblem,
        RewindSettingsFileParseProblem,
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

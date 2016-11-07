using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings
{
    public class SettingsContext : ISettingsContext
    {
        private ISettingsService<PlayerPanelSettings> playerPanelSettingsService;
        private ISettingsService<ChallengeSettings> challengeSettingsService;
        private ISettingsService<FtpSettings> ftpSettingsService;
        private ISettingsService<RewindSettings> rewindSettingsService;

        public SettingsContext(ISettingsService<PlayerPanelSettings> playerPanelSettingsService,
                               ISettingsService<ChallengeSettings> challengeSettingsService,
                               ISettingsService<FtpSettings> ftpSettingsService,
                               ISettingsService<RewindSettings> rewindSettingsService)
        {
            this.playerPanelSettingsService = playerPanelSettingsService;
            this.challengeSettingsService = challengeSettingsService;
            this.ftpSettingsService = ftpSettingsService;
            this.rewindSettingsService = rewindSettingsService;    
        }

        public PlayerPanelSettings GetPlayerPanelSetting()
        {
            return playerPanelSettingsService.GetSetting();
        }

        public void SavePlayerPanelSetting(PlayerPanelSettings playerPanelSetting)
        {
            playerPanelSettingsService.SaveSetting(playerPanelSetting);
        }

        public ChallengeSettings GetChallengeSetting()
        {
            return challengeSettingsService.GetSetting();
        }

        public void SaveChallengeSetting(ChallengeSettings challengeSetting)
        {
            challengeSettingsService.SaveSetting(challengeSetting);
        }

        public FtpSettings GetFtpSetting()
        {
            return ftpSettingsService.GetSetting();
        }

        public void SaveFtpSetting(FtpSettings ftpSetting)
        {
            ftpSettingsService.SaveSetting(ftpSetting);
        }

        public RewindSettings GetRewindSetting()
        {
            return rewindSettingsService.GetSetting();
        }

        public void SaveRewindSetting(RewindSettings rewindSetting)
        {
            rewindSettingsService.SaveSetting(rewindSetting);
        }
    }
}

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
        private PlayerPanelSettings playerPanelSetting;
        private ChallengeSettings challengeSetting;
        private FtpSettings ftpSetting;
        private RewindSettings rewindSetting;

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

        public PlayerPanelSettings PlayerPanelSetting
        {
            get
            {
                return playerPanelSetting;
            }
        }

        public ChallengeSettings ChallengeSetting
        {
            get
            {
                return challengeSetting;
            }
        }

        public FtpSettings FtpSetting
        {
            get
            {
                return ftpSetting;
            }
        }

        public RewindSettings RewindSetting
        {
            get
            {
                return rewindSetting;
            }
        }

        public PlayerPanelSettings GetPlayerPanelSetting()
        {
            playerPanelSetting = playerPanelSettingsService.GetSetting();
            return playerPanelSetting;
        }

        public void SavePlayerPanelSetting(PlayerPanelSettings playerPanelSetting)
        {
            this.playerPanelSetting = playerPanelSetting;
            playerPanelSettingsService.SaveSetting(playerPanelSetting);
        }

        public ChallengeSettings GetChallengeSetting()
        {
            challengeSetting = challengeSettingsService.GetSetting();
            return challengeSetting;
        }

        public void SaveChallengeSetting(ChallengeSettings challengeSetting)
        {
            this.challengeSetting = challengeSetting;
            challengeSettingsService.SaveSetting(challengeSetting);
        }

        public FtpSettings GetFtpSetting()
        {
            ftpSetting = ftpSettingsService.GetSetting();
            return ftpSetting;
        }

        public void SaveFtpSetting(FtpSettings ftpSetting)
        {
            this.ftpSetting = ftpSetting;
            ftpSettingsService.SaveSetting(ftpSetting);
        }

        public RewindSettings GetRewindSetting()
        {
            rewindSetting = rewindSettingsService.GetSetting();
            return rewindSetting;
        }

        public void SaveRewindSetting(RewindSettings rewindSetting)
        {
            this.rewindSetting = rewindSetting;
            rewindSettingsService.SaveSetting(rewindSetting);
        }
    }
}

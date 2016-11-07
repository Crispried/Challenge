using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings
{
    public class NullSettingsContainer : INullSettingsContainer
    {
        private List<Type> nullSettingTypes;
        
        public NullSettingsContainer()
        {
            nullSettingTypes = new List<Type>();
        } 

        public List<Type> NullSettingTypes
        {
            get
            {
                return nullSettingTypes;
            }
        }

        public bool IsEmpty()
        {
            return nullSettingTypes.Count == 0 ? true : false;
        }

        public void CheckPlayerPanelSettingOnNull(PlayerPanelSettings playerPanelSetting)
        {
            if (playerPanelSetting == null)
            {
                Add(typeof(PlayerPanelSettings));
            }
        }

        public void CheckChallengeSettingOnNull(ChallengeSettings challengeSettings)
        {
            if (challengeSettings == null)
            {
                Add(typeof(ChallengeSettings));
            }
        }

        public void CheckFtpSettingOnNull(FtpSettings ftpSettings)
        {
            if (ftpSettings == null)
            {
                Add(typeof(FtpSettings));
            }
        }

        public void CheckRewindSettingOnNull(RewindSettings rewindSettings)
        {
            if (rewindSettings == null)
            {
                Add(typeof(RewindSettings));
            }
        }

        private void Add(Type type)
        {
            if (!nullSettingTypes.Contains(type))
            {
                nullSettingTypes.Add(type);
            }
        }
    }
}

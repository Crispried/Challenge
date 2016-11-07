using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings
{
    public interface ISettingsService<T> where T : Setting
    {
        void SaveSetting(T setting);

        T GetSetting();
    }
}

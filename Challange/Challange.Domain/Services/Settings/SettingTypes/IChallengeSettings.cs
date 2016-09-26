using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Settings.SettingTypes
{
    public interface IChallengeSettings
    {
        int NumberOfPastFPS { get; set; }

        int NumberOfFutureFPS { get; set; }
    }
}

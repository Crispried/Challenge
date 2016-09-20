using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Views
{
    public interface IChallengeSettingsView : IView
    {
        event Action ChangeChallengeSettings;

        ChallengeSettings ChallengeSettings { get; set; }

        void SetChallengeSettings(
                    ChallengeSettings challengeSettings);
        
        bool ValidateForm();

        void ShowValidationErrorMessage();
    }
}

using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Drawing;

namespace Challange.Presenter.Views
{
    public interface IFtpSettingsView : IView
    {
        event Action<FtpSettings> ChangeFtpSettings;

        event Action<FtpSettings> TestFtpConnection;

        void SetFtpSettings(FtpSettings ftpSettings);

        FtpSettings FtpSettings { get; set; }

        bool ValidateForm();

        void ShowMessage(ChallengeMessage message);
    }
}

using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;
using System.Drawing;

namespace Challange.Presenter.Views
{
    public interface IFtpView : IView
    {
        event Action ChangeFtpSettings;

        void SetFtpSettings(FtpSettings ftpSettings);

        FtpSettings FtpSettings { get; set; }

        bool ValidateForm();

        void ShowValidationErrorMessage();
    }
}

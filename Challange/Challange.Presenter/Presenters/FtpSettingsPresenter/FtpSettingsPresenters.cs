using Challange.Domain.Services.Ftp.Abstract;
using Challange.Domain.Services.Ftp.Concrete;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Presenter.Presenters.FtpSettingsPresenter
{
    public partial class FtpSettingsPresenter
    { 
        public override void Run(FtpSettings argument)
        {
            ftpSettings = argument;
            SetFtpSettings();
            View.Show();
        }

        /// <summary>
        /// Changes challenge settings (number of past and future FPSes)
        /// </summary>
        public void ChangeFtpSettings(FtpSettings newSettings)
        {
            if (View.ValidateForm())
            {
                SaveSettings(newSettings);
                ftpSettings.SetSettings(newSettings);
                View.Close();
            }
            else
            {
                ChallengeMessage message =
                    messageParser.GetMessage(MessageType.FtpSettingsInvalid);
                View.ShowMessage(message);
            }
        }

        public void TestFtpConnection(FtpSettings ftpSettings)
        {
            IFtpWorker ftpWorker = new FtpWorker(
                                    ftpSettings.FtpAddress,
                                    ftpSettings.UserName,
                                    ftpSettings.Password);
            bool ftpConnectionSuccess = ftpWorker.IsFtpConnectionSuccessful();
            ChallengeMessage message = ftpConnectionSuccess ?
                messageParser.GetMessage(MessageType.TestFtpConnectionSuccessed) :
                messageParser.GetMessage(MessageType.TestFtpConnectionFailed);
            View.ShowMessage(message);
        }
    }
}

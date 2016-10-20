using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.FtpPresenter
{
    public partial class FtpPresenter : BasePresenter<IFtpView, FtpSettings>
    {
        private FtpSettings ftpSettings;

        public FtpPresenter(
                IApplicationController controller,
                IFtpView ftpView) :
                base(controller, ftpView)
        {
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.ChangeFtpSettings += ChangeFtpSettings;
            View.TestFtpConnection += TestFtpConnection;
        }
    }
}

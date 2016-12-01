using Challange.Domain.Entities;
using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.GameInformationPresenter
{
    public partial class GameInformationPresenter :
                    BasePresenter<IGameInformationView, GameInformation>
    {
        private GameInformation gameInformation;

        private IFileService fileService;

        private IFileWorker fileWorker;

        private IPathFormatter pathFormatter;

        public GameInformationPresenter(
        IApplicationController controller,
        IGameInformationView gameInformationView,
        IFileService fileService,
        IFileWorker fileWorker,
        IPathFormatter pathFormatter
        ) :
                base(controller, gameInformationView)
        {
            this.fileService = fileService;
            this.fileWorker = fileWorker;
            this.pathFormatter = pathFormatter;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.SetGameInformation += PrepareApplication;
        }
    }
}

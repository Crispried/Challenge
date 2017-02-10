using Challange.Domain.Services.Challenge;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Views;

namespace Challange.Presenter.Presenters.GameInformationPresenter
{
    public partial class GameInformationPresenter :
                    BasePresenter<IGameInformationView, GameInformation>
    {
        private GameInformation gameInformation;

        private IFileService fileService;

        private IXmlWorker fileWorker;

        private IPathFormatter pathFormatter;

        public GameInformationPresenter(
        IApplicationController controller,
        IGameInformationView gameInformationView,
        IFileService fileService,
        IXmlWorker fileWorker,
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

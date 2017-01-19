using Challange.Domain.Services.Challenge;
using Challange.Presenter.Base;
using System;

namespace Challange.Presenter.Views
{
    public interface IGameInformationView : IView
    {
        event Action<GameInformation> SetGameInformation;
    }
}

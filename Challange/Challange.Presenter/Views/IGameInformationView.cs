using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using System;

namespace Challange.Presenter.Views
{
    public interface IGameInformationView : IView
    {
        event Action SetGameInformation;

        GameInformation GameInformation { get; }
    }
}

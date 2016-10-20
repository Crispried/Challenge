using Challange.Domain.Services.Message;
using Challange.Presenter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Views
{
    public interface IChallengePlayerView : IView
    {
        event Action OpenRewindSettings;

        void ShowMessage(ChallengeMessage message);
    }
}

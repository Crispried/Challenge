using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Concrete;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    {
        [ExcludeFromCodeCoverage]
        public void DrawAction(Bitmap frame, string videoName)
        {
            View.DrawNewFrame(frame, videoName);
        }
    }
}

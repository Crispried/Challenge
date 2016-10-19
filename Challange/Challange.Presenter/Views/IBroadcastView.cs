using Challange.Presenter.Base;
using System;
using System.Drawing;

namespace Challange.Presenter.Views
{
    public interface IBroadcastView : IView
    {
        event Action BroadcastShowCallback;
        void DrawNewFrame(Bitmap frame, string fullCameraName);
    }
}

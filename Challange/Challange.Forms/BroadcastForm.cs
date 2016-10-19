using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class BroadcastForm : Form, IBroadcastView
    {
        public BroadcastForm()
        {
            InitializeComponent();
            this.Shown += (sender, args) => Invoke(BroadcastShowCallback);
        }

        public event Action BroadcastShowCallback;

        public void DrawNewFrame(Bitmap frame, string fullCameraName)
        {
            Bitmap frameClone = CloneFrame(frame);
            broadcastPictureBox.Image = frameClone;
        }

        private Bitmap CloneFrame(Bitmap frame)
        {
            return frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), frame.PixelFormat);
        }
    }
}

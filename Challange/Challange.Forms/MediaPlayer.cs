using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuartzTypeLib;

namespace Challange.Forms
{
    public partial class MediaPlayer : UserControl
    {
        internal const int
 WS_CHILD = 0x40000000,
 WS_VISIBLE = 0x10000000,
 LBS_NOTIFY = 0x00000001,
 HOST_ID = 0x00000002,
 LISTBOX_ID = 0x00000001,
 WS_VSCROLL = 0x00200000,
 WS_BORDER = 0x00800000;

        private FilgraphManager graphManager = new FilgraphManager();
        private IMediaControl mControl;
        private IMediaPosition mPosition;
        private IVideoWindow mWindow;
        public MediaPlayer()
        {
        }
        public bool LoadFile(string sfile, Panel parentHandler, int width, int height)
        {
            graphManager.RenderFile(sfile);
            mControl = graphManager;
            mPosition = graphManager as IMediaPosition;
            mWindow = graphManager as IVideoWindow;
            mWindow.Owner = parentHandler.Handle.ToInt32();
            mWindow.WindowStyle = WS_CHILD;
            mWindow.SetWindowPosition(parentHandler.Left,
                parentHandler.Top + 20,
                width,
                height);
            graphManager.Run();
            return true;
        }
    }
}

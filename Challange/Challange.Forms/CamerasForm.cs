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
    public partial class CamerasForm : Form, ICamerasView
    {
        public CamerasForm()
        {
            InitializeComponent();
        }

        public new void Show()
        {
            ShowDialog();
        }

        public void FillCamerasListView(List<string> camerasNames)
        {
            foreach (string cameraName in camerasNames)
            {
                camerasListBox.Items.Add(cameraName);
            }
        }
    }
}

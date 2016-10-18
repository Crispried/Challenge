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
        }

        public new void Show()
        {
            ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms.Infrastructure
{
    public static class ControlsHelper
    {
        public static IEnumerable<T> GetControls<T>(Control parent) where T : Control
        {
            return parent.Controls.Cast<T>();
        }

        public static T GetFirstControl<T>(Control parent) where T : Control
        {
            return parent.Controls.Cast<T>().FirstOrDefault();
        }

        public static T GetFirstControlWithName<T>(Control parent, string name) where T : Control
        {
            return (T)parent.Controls.Find(name, false).FirstOrDefault();
        }

        public static int GetControlIndex(Control parent, Control child)
        {
            return parent.Controls.GetChildIndex(child);
        }

        public static void SetChildIndex(Control parent, Control child, int index)
        {
            parent.Controls.SetChildIndex(child, index);
        }

        public static void RemoveControl(Control parent, Control child)
        {
            parent.Controls.Remove(child);
        }

        public static void RemoveAllControls(Control control)
        {
            control.Controls.Clear();
        }

        public static void AddControl(Control parent, Control child)
        {
            parent.Controls.Add(child);
        }

        public static void HideControl(Control control)
        {
            control.Hide();
        }

        public static void ShowControl(Control control)
        {
            control.Show();
        }

        public static void SetBorderStyle(PictureBox pictureBox, BorderStyle borderStyle)
        {
            pictureBox.BorderStyle = borderStyle;
        }

        public static void SetCursor(Control control, Cursor cursorType)
        {
            control.Cursor = cursorType;
        }

        public static void SetWindowState(Form form, FormWindowState state)
        {
            form.WindowState = state;
        }

        public static void SetDock(Control control, DockStyle style)
        {
            control.Dock = style;
        }
    }
}

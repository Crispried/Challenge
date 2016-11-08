using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICamera
    {
        Bitmap GetCurrentFrame { get; }
        string FullName { get; }
        string Name { get; set; }
        event EventHandler<NewFrameEventArgs> NewFrameEvent;
        void Start();
        void Stop();
        bool Equals(object obj);
        bool Equals(ICamera camera);
        int GetHashCode();
    }
}

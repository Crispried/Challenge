using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Challange.Domain.Entities;

namespace Challange.Domain.Abstract
{
    public interface ICamera
    {
        Bitmap GetCurrentFrame { get; }

        string FullName { get; }

        string Name { set; }

        void Start();

        void Stop();

        bool Equals(object obj);

        bool Equals(Camera camera);

        int GetHashCode();
    }
}

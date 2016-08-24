using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICameraProvider
    {
        /// <summary>
        /// returns all names of connected cameras
        /// </summary>
        /// <returns>List of type string</returns>
        List<string> GetConnectedCameras();


    }
}

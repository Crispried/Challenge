using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;

namespace Challange.Domain.Abstract
{
    public interface IChallengeBuffer
    {
        Dictionary<string, List<IFps>> PastCameraRecords { get; }

        Dictionary<string, List<IFps>> FutureCameraRecords { get; }

        List<IFps> GetPastCameraRecordsValueByKey(string key);

        List<IFps> GetFutureCameraRecordsValueByKey(string key);

        List<IFps> GetFirstPastValue();

        List<IFps> GetFirstFutureValue();

        void ClearBuffers();

        void RemoveFirstFpsFromPastBuffer();
    }
}

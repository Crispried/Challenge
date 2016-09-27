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
        Dictionary<string, List<Fps>> PastCameraRecords { get; }

        Dictionary<string, List<Fps>> FutureCameraRecords { get; }

        void AddNewPastCameraRecord(string key, List<Fps> value);

        void AddNewFutureCameraRecord(string key, List<Fps> value);

        List<Fps> GetPastCameraRecordsValueByKey(string key);

        List<Fps> GetFutureCameraRecordsValueByKey(string key);

        List<Fps> GetFirstPastValue();

        List<Fps> GetFirstFutureValue();

        void ClearBuffers();

        void RemoveFirstFpsFromPastBuffer();
    }
}

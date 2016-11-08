using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Abstract;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeBuffer
    {
        int MaxElementsInPastCollection { get; set; }

        int MaxElementsInFutureCollection { get; set; }

        Dictionary<string, List<IFps>> PastCameraRecords { get; }

        Dictionary<string, List<IFps>> FutureCameraRecords { get; }

        List<IFps> GetPastCameraRecordsValueByKey(string key);

        List<IFps> GetFutureCameraRecordsValueByKey(string key);

        List<IFps> GetFirstPastValue();

        List<IFps> GetFirstFutureValue();

        void ClearBuffers();

        void RemoveFirstFpsFromPastBuffer();

        void AddPastFpses(IFpsContainer fpsContainer);

        void AddFutureFpses(IFpsContainer fpsContainer);

        bool HaveToRemovePastFps();

        bool HaveToAddFutureFps();
    }
}

using Challange.Domain.Services.StreamProcess.Abstract;
using System.Collections.Generic;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeBuffers
    {
        Dictionary<string, List<IFps>> PastCameraRecords { get; }

        Dictionary<string, List<IFps>> FutureCameraRecords { get; }     

        void ClearBuffers();

        Dictionary<string, List<IFps>> UniteBuffers(ICamerasContainer camerasContainer);

        void RemoveFirstFpsFromPastBuffer();

        void AddPastFpses(IFpsContainer fpsContainer);

        void AddFutureFpses(IFpsContainer fpsContainer);

        bool HaveToRemovePastFps(int maxElementsInPastCollection);

        bool HaveToAddFutureFps(int maxElementsInFutureCollection);
    }
}

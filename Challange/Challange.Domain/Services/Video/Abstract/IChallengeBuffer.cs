using System.Collections.Generic;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeBuffers
    {
        Dictionary<string, List<IFps>> PastCameraRecords { get; }

        Dictionary<string, List<IFps>> FutureCameraRecords { get; }     

        void ClearBuffers();

        Dictionary<string, List<IFps>> UniteBuffers();

        void RemoveFirstFpsFromPastBuffer();

        void AddPastFpses(IFpsContainer fpsContainer);

        void AddFutureFpses(IFpsContainer fpsContainer);

        bool HaveToRemovePastFps(int maxElementsInPastCollection);

        bool HaveToAddFutureFps(int maxElementsInFutureCollection);
    }
}

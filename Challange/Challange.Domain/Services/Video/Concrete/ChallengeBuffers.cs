using System.Collections.Generic;
using System.Linq;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.Domain.Services.Video.Concrete
{
    public class ChallengeBuffers : IChallengeBuffers
    {
        private Dictionary<string, List<IFps>> pastCameraRecords;
        private Dictionary<string, List<IFps>> futureCameraRecords;

        public ChallengeBuffers()
        {
            pastCameraRecords = new Dictionary<string, List<IFps>>();
            futureCameraRecords = new Dictionary<string, List<IFps>>();
        }

        public Dictionary<string, List<IFps>> PastCameraRecords
        {
            get
            {
                return pastCameraRecords;
            }
        }

        public Dictionary<string, List<IFps>> FutureCameraRecords
        {
            get
            {
                return futureCameraRecords;
            }
        }

        public void ClearBuffers()
        {
            pastCameraRecords.Clear();
            futureCameraRecords.Clear();
        }

        public Dictionary<string, List<IFps>> UniteBuffers(ICamerasContainer camerasContainer)
        {
            var unitedBuffers = new Dictionary<string, List<IFps>>();
            List<IFps> tempUnitedFpsList;
            foreach (var pastFrames in pastCameraRecords)
            {
                foreach (var futureFrames in futureCameraRecords)
                {
                    if (pastFrames.Key == futureFrames.Key)
                    {
                        tempUnitedFpsList = new List<IFps>();
                        tempUnitedFpsList.AddRange(pastFrames.Value);
                        tempUnitedFpsList.AddRange(futureFrames.Value);
                        var camera = camerasContainer.GetCameraByKey(pastFrames.Key);
                        var cameraName = camera.Name;
                        unitedBuffers.Add(cameraName, tempUnitedFpsList);
                        break;
                    }
                }
            }
            return unitedBuffers;
        }

        /// <summary>
        /// for example our past buffer is only for 20 FPS objects
        /// so on 21 second we have to remove the first object from this buffer
        /// </summary>
        public void RemoveFirstFpsFromPastBuffer()
        {
            foreach (var pastCameraRecordFpsList in pastCameraRecords.Values)
            {
                pastCameraRecordFpsList.Remove(pastCameraRecordFpsList.FirstOrDefault());
            }
        }

        /// <summary>
        /// check is past frame buffer count equals
        /// to necessary number of past FPS
        /// </summary>
        /// <returns></returns>
        public bool HaveToRemovePastFps(int maxElementsInPastCollection)
        {
            var pastFrames = GetFirstPastValue();
            if (pastFrames != null)
            {
                return pastFrames.Count == maxElementsInPastCollection;
            }
            return false;
        }

        /// <summary>
        /// check is future frame buffer count equals
        /// to necessary number of future FPS
        /// </summary>
        /// <returns></returns>
        public bool HaveToAddFutureFps(int maxElementsInFutureCollection)
        {
            var futureFrames = GetFirstFutureValue();
            if(futureFrames != null)
            {
                return futureFrames.Count != maxElementsInFutureCollection;
            }
            return false;
        }

        /// <summary>
        /// adds past fps objects into buffer for past frames
        /// </summary>
        public void AddPastFpses(IFpsContainer fpsContainer)
        {
            foreach (var fps in fpsContainer.Fpses)
            {
                var temp = GetPastRecordValueByKey(fps.Key);
                temp.Add(fps.Value);
            }
        }

        /// <summary>
        /// adds future fps objects into buffer for future frames
        /// </summary>
        public void AddFutureFpses(IFpsContainer fpsContainer)
        {
            foreach (var fps in fpsContainer.Fpses)
            {
                var temp = GetFutureRecordValueByKey(fps.Key);
                temp.Add(fps.Value);
            }
        }

        private List<IFps> GetFirstPastValue()
        {
            return pastCameraRecords.Values.FirstOrDefault();
        }

        private List<IFps> GetFirstFutureValue()
        {
            return futureCameraRecords.Values.FirstOrDefault();
        }

        private List<IFps> GetPastRecordValueByKey(string key)
        {
            if (pastCameraRecords.ContainsKey(key))
            {
                return pastCameraRecords[key];
            }
            else
            {
                var fpsList = new List<IFps>();
                pastCameraRecords.Add(key, fpsList);
                return fpsList;
            }
        }

        private List<IFps> GetFutureRecordValueByKey(string key)
        {
            if (futureCameraRecords.ContainsKey(key))
            {
                return futureCameraRecords[key];
            }
            else
            {
                var fpsList = new List<IFps>();
                futureCameraRecords.Add(key, fpsList);
                return fpsList;               
            }
        }
    }
}

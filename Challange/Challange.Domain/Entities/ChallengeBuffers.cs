using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Domain.Entities
{
    public class ChallengeBuffers : IChallengeBuffer
    {
        private Dictionary<string, List<Fps>> pastCameraRecords;
        private Dictionary<string, List<Fps>> futureCameraRecords;
        private int maxElementsInPastCollection;
        private int maxElementsInFutureCollection;

       // private IChallengeSettings challengeSettings;

        public ChallengeBuffers(CamerasContainer camerasContainer,
            int maxElementsInPastCollection, int maxElementsInFutureCollection)
        {
            this.maxElementsInPastCollection = maxElementsInPastCollection;
            this.maxElementsInFutureCollection = maxElementsInFutureCollection;
            InitializeBuffers(camerasContainer);
        }

        public Dictionary<string, List<Fps>> PastCameraRecords
        {
            get
            {
                return pastCameraRecords;
            }
        }

        public Dictionary<string, List<Fps>> FutureCameraRecords
        {
            get
            {
                return futureCameraRecords;
            }
        }

        public void AddNewPastCameraRecord(string key, List<Fps> value)
        {
            AddNewRecord(key, value, pastCameraRecords);
        }

        public void AddNewFutureCameraRecord(string key, List<Fps> value)
        {
            AddNewRecord(key, value, futureCameraRecords);
        }

        private void AddNewRecord(string key, List<Fps> value,
                            Dictionary<string, List<Fps>> dictionary)
        {
            dictionary.Add(key, value);
        }

        public List<Fps> GetPastCameraRecordsValueByKey(string key)
        {
            return GetByValue(key, pastCameraRecords);
        }

        public List<Fps> GetFutureCameraRecordsValueByKey(string key)
        {
            return GetByValue(key, futureCameraRecords);
        }

        public List<Fps> GetFirstPastValue()
        {
            return pastCameraRecords.Values.FirstOrDefault();
        }

        public List<Fps> GetFirstFutureValue()
        {
            return futureCameraRecords.Values.FirstOrDefault();
        }

        private List<Fps> GetByValue(string key,
                        Dictionary<string, List<Fps>> dictionary)
        {
            List<Fps> value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        public void ClearBuffers()
        {
            pastCameraRecords.Clear();
            futureCameraRecords.Clear();
        }

        public void RemoveFirstFpsFromPastBuffer()
        {
            var fpsesToRemove = new Dictionary<string, Fps>();
            foreach (var pastFrames in pastCameraRecords)
            {
                fpsesToRemove.Add(pastFrames.Key, pastFrames.Value[0]);
                pastFrames.Value.RemoveAt(0);
            }
            foreach (var fpsToRemove in fpsesToRemove.Values)
            {
                foreach (var frame in fpsToRemove.Frames)
                {
                    frame.Dispose();
                }
            }
        }

        /// <summary>
        /// check is past frame buffer count equals
        /// to necessary number of past FPS
        /// </summary>
        /// <returns></returns>
        public bool HaveToRemovePastFps()
        {
            var pastFrames = GetFirstPastValue();
            return pastFrames.Count == maxElementsInPastCollection;
        }

        /// <summary>
        /// check is future frame buffer count equals
        /// to necessary number of future FPS
        /// </summary>
        /// <returns></returns>
        public bool HaveToAddFutureFps()
        {
            var futureFrames = GetFirstFutureValue();
            return futureFrames.Count != maxElementsInFutureCollection;
        }


        /// <summary>
        /// adds past fps objects into buffer for past frames
        /// </summary>
        public void AddPastFpses(FpsContainer fpsContainer)
        {
            List<Fps> temp;
            foreach (var fps in fpsContainer.Fpses)
            {
                temp = GetPastCameraRecordsValueByKey(fps.Key);
                if (temp != null)
                {
                    temp.Add(fps.Value);
                }
                else
                {
                    temp = new List<Fps>();
                    temp.Add(fps.Value);
                    AddNewPastCameraRecord(fps.Key, temp);
                }
            }
        }

        /// <summary>
        /// adds future fps objects into buffer for future frames
        /// </summary>
        public void AddFutureFpses(FpsContainer fpsContainer)
        {
            List<Fps> temp;
            foreach (var fps in fpsContainer.Fpses)
            {
                temp = GetFutureCameraRecordsValueByKey(fps.Key);
                if (temp != null)
                {
                    temp.Add(fps.Value);
                }
                else
                {
                    temp = new List<Fps>();
                    temp.Add(fps.Value);
                    AddNewFutureCameraRecord(fps.Key, temp);
                }
            }
        }

        /// <summary>
        /// Initialize 2 buffers for past and future frames
        /// </summary>
        private void InitializeBuffers(CamerasContainer camerasContainer)
        {
            pastCameraRecords = new Dictionary<string, List<Fps>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                pastCameraRecords.Add(camera.FullName, new List<Fps>());
            }
            futureCameraRecords = new Dictionary<string, List<Fps>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                futureCameraRecords.Add(camera.FullName, new List<Fps>());
            }
        }
    }
}

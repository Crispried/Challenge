using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;

namespace Challange.Domain.Entities
{
    public class ChallengeBuffers : IChallengeBuffer
    {
        private Dictionary<string, List<Fps>> pastCameraRecords;
        private Dictionary<string, List<Fps>> futureCameraRecords;

        public ChallengeBuffers(CamerasContainer camerasContainer)
        {
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

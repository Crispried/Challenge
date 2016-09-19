using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class ChallengeBuffers
    {
        private Dictionary<string, List<FPS>> pastCameraRecords;
        private Dictionary<string, List<FPS>> futureCameraRecords;

        public ChallengeBuffers(CamerasContainer<Camera> camerasContainer)
        {
            InitializeBuffers(camerasContainer);
        }

        public Dictionary<string, List<FPS>> PastCameraRecords
        {
            get
            {
                return pastCameraRecords;
            }
        }

        public Dictionary<string, List<FPS>> FutureCameraRecords
        {
            get
            {
                return futureCameraRecords;
            }
        }

        public void AddNewPastCameraRecord(string key, List<FPS> value)
        {
            AddNewRecord(key, value, pastCameraRecords);
        }

        public void AddNewFutureCameraRecord(string key, List<FPS> value)
        {
            AddNewRecord(key, value, futureCameraRecords);
        }

        private void AddNewRecord(string key, List<FPS> value,
                            Dictionary<string, List<FPS>> dictionary)
        {
            dictionary.Add(key, value);
        }

        public List<FPS> GetPastCameraRecordsValueByKey(string key)
        {
            return GetByValue(key, pastCameraRecords);
        }

        public List<FPS> GetFutureCameraRecordsValueByKey(string key)
        {
            return GetByValue(key, futureCameraRecords);
        }

        public List<FPS> GetFirstPastValue()
        {
            return pastCameraRecords.Values.FirstOrDefault();
        }

        public List<FPS> GetFirstFutureValue()
        {
            return futureCameraRecords.Values.FirstOrDefault();
        }

        private List<FPS> GetByValue(string key,
                        Dictionary<string, List<FPS>> dictionary)
        {
            List<FPS> value;
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

        /// <summary>
        /// Initialize 2 buffers for past and future frames
        /// </summary>
        private void InitializeBuffers(CamerasContainer<Camera> camerasContainer)
        {
            pastCameraRecords = new Dictionary<string, List<FPS>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                pastCameraRecords.Add(camera.FullName, new List<FPS>());
            }
            futureCameraRecords = new Dictionary<string, List<FPS>>();
            foreach (Camera camera in camerasContainer.GetCameras)
            {
                futureCameraRecords.Add(camera.FullName, new List<FPS>());
            }
        }
    }
}

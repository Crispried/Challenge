using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.Domain.Servuces.Video.Concrete
{
    public class ChallengeBuffers : IChallengeBuffers
    {
        private Dictionary<string, List<IFps>> pastCameraRecords;
        private Dictionary<string, List<IFps>> futureCameraRecords;
        private int maxElementsInPastCollection = 20;
        private int maxElementsInFutureCollection = 20;

        public ChallengeBuffers(ICamerasContainer camerasContainer)
        {
            InitializeBuffers(camerasContainer);
        }

        public int MaxElementsInPastCollection
        {
            get
            {
                return maxElementsInPastCollection;
            }
            set
            {
                maxElementsInPastCollection = value;
            }
        }

        public int MaxElementsInFutureCollection
        {
            get
            {
                return maxElementsInFutureCollection;
            }
            set
            {
                maxElementsInFutureCollection = value;
            }
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

        public void SetNumberOfPastAndFutureElements(int maxElementsInPastCollection,
                                              int maxElementsInFutureCollection)
        {
            this.maxElementsInPastCollection = maxElementsInPastCollection;
            this.maxElementsInFutureCollection = maxElementsInFutureCollection;
        }

        public List<IFps> GetPastCameraRecordsValueByKey(string key)
        {
            return GetKeyByValue(key, pastCameraRecords);
        }

        public List<IFps> GetFutureCameraRecordsValueByKey(string key)
        {
            return GetKeyByValue(key, futureCameraRecords);
        }

        public List<IFps> GetFirstPastValue()
        {
            return pastCameraRecords.Values.FirstOrDefault();
        }

        public List<IFps> GetFirstFutureValue()
        {
            return futureCameraRecords.Values.FirstOrDefault();          
        }

        public void ClearBuffers()
        {
            pastCameraRecords.Clear();
            futureCameraRecords.Clear();
        }

        /// <summary>
        /// for example our past buffer is only for 20 FPS objects
        /// so on 21 second we have to remove the first object from this buffer
        /// </summary>
        public void RemoveFirstFpsFromPastBuffer()
        {
            var fpsesToRemove = new Dictionary<string, IFps>();
            foreach (var pastFrames in pastCameraRecords)
            {
                fpsesToRemove.Add(pastFrames.Key, pastFrames.Value[0]);
                pastFrames.Value.RemoveAt(0);
            }
            foreach (var fpsToRemove in fpsesToRemove.Values)
            {
                fpsToRemove.DisposeFrames();
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
        public bool HaveToAddFutureFps()
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
            List<IFps> temp;
            foreach (var fps in fpsContainer.Fpses)
            {
                temp = GetPastCameraRecordsValueByKey(fps.Key);
                if (temp != null)
                {
                    temp.Add(fps.Value);
                }
                else
                {
                    temp = new List<IFps>();
                    temp.Add(fps.Value);
                    AddNewPastCameraRecord(fps.Key, temp);
                }
            }
        }

        /// <summary>
        /// adds future fps objects into buffer for future frames
        /// </summary>
        public void AddFutureFpses(IFpsContainer fpsContainer)
        {
            List<IFps> temp;
            foreach (var fps in fpsContainer.Fpses)
            {
                temp = GetFutureCameraRecordsValueByKey(fps.Key);
                if (temp != null)
                {
                    temp.Add(fps.Value);
                }
                else
                {
                    temp = new List<IFps>();
                    temp.Add(fps.Value);
                    AddNewFutureCameraRecord(fps.Key, temp);
                }
            }
        }

        private void AddNewPastCameraRecord(string key, List<IFps> value)
        {
            AddNewRecord(key, value, pastCameraRecords);
        }

        private void AddNewFutureCameraRecord(string key, List<IFps> value)
        {
            AddNewRecord(key, value, futureCameraRecords);
        }

        private void AddNewRecord(string key, List<IFps> value,
                            Dictionary<string, List<IFps>> dictionary)
        {
            dictionary.Add(key, value);
        }

        private List<IFps> GetKeyByValue(string key,
                        Dictionary<string, List<IFps>> dictionary)
        {
            List<IFps> value;
            return dictionary.TryGetValue(key, out value) ? value : null;
        }

        /// <summary>
        /// Initialize 2 buffers for past and future frames
        /// </summary>
        private void InitializeBuffers(ICamerasContainer camerasContainer)
        {
            pastCameraRecords = new Dictionary<string, List<IFps>>();
            foreach (string key in camerasContainer.GetCamerasKeys)
            {
                pastCameraRecords.Add(key, new List<IFps>());
            }
            futureCameraRecords = new Dictionary<string, List<IFps>>();
            foreach (string key in camerasContainer.GetCamerasKeys)
            {
                futureCameraRecords.Add(key, new List<IFps>());
            }
        }
    }
}

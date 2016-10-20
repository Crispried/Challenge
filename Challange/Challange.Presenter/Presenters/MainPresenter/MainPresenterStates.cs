using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter
    {
        #region General
        public bool IsDeviceListEmpty { get; set; }

        public bool IsStreamProcessOn { get; set; }

        public bool IsCaptureDevicesEnable { get; set; }

        public bool IsEventForPastFramesActive { get; set; }

        public bool IsEventForFutureFramesActive { get; set; }

        public bool IsChallengeButtonVisible { get; set; }

        public bool IsChallengeRecordingImageVisible { get; set; }
        #endregion

        #region Run
        public bool ChallengeSettingsAreNull { get; set; }

        public bool PlayerPanelSettingsAreNull { get; set; }

        public bool FtpSettingsAreNull { get; set; }
        #endregion

        #region Start stream
        public bool AreCamerasBindedToPlayers { get; set; }

        public bool WasTimeAxisTimerInitialized { get; set; }

        public bool WasRecordingFpsTimerInitialized { get; set; }

        public bool WasDeviceListEmptyMessageShowed { get; set; }
        #endregion

        #region Stop stream
        public bool WasTimeAxisResetted { get; set; }
        #endregion

        #region Create challange
        public bool ElapsedTimeWasGot { get; set; }

        public bool DirectoryForChallengeWasCreated { get; set; }

        public bool MarkerWasAddedOntoTimeAxis { get; set; }
        #endregion
    }
}

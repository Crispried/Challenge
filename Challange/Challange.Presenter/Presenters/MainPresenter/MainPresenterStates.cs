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

        #endregion

        #region Run
        public bool ChallengeSettingsAreNull { get; set; }

        public bool PlayerPanelSettingsAreNull { get; set; }
        #endregion

        #region Stop stream
        public bool IsCaptureDevicesEnable { get; set; }

        public bool IsTimeAxisResetted { get; set; }

        public bool IsStreamProcessOn { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengeSettingsPresenter
{
    public partial class ChallengeSettingsPresenter
    {
        #region General

        #endregion

        #region Run
        public bool ChallengeSettingsAreOpened { get; set; }
        #endregion

        #region Save settings
        public bool ChallengeSettingsAreSaved { get; set; }
        #endregion

        #region Form validation
        public bool ChallengeSettingsFormIsValid { get; set; }

        public bool ChallengeSettingsFormIsInvalid { get; set; }
        #endregion
    }
}

using Challange.Presenter.Base;
using Challange.Presenter.Presenters.ChallengePlayerPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    public class ChallengePlayerPresenterTest
    {
        private IApplicationController controller;
        private ChallengePlayerPresenter presenter;
        private IChallengePlayerView view;
        private string mock;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IChallengePlayerView>();
            presenter = new ChallengePlayerPresenter(controller, view);
            presenter.Run(mock);
        }
    }
}

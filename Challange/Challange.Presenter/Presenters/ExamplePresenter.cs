using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Entities;

namespace Challange.Presenter.Presenters
{
    public class ExamplePresenter : BasePresenter<IExampleView, ExampleObject>
    {
        private ExampleObject exampleObject;

        public ExamplePresenter(IApplicationController controller,
                                IExampleView exampleView) : base(controller, exampleView)
        {
            
        }

        public override void Run(ExampleObject argument)
        {
            exampleObject = argument;
            View.Show();
        }
    }
}

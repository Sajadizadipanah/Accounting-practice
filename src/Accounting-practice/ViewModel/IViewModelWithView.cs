using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_practice.ViewModel;

public interface IViewModelWithView : IViewModel
{
    DependencyObject? View
    {
        get;
        set;
    }

    void OnViewLoaded();
    void OnViewUnloaded();
}

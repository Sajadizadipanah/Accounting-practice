using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Accounting_practice.ViewModel;

public interface IViewModelWithWindow : IViewModelWithView
{
    IAppWindow? GetWindow();
}

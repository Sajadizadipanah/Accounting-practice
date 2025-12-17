using Accounting_practice.Registration;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_practice.MainWindow;

public partial class MainWindowViewModel
{

    private LoanRegistrationPage? _loanRegistrationPage;
    [RelayCommand]
    private void GoToLoanRegistrationPage()
    {
        _loanRegistrationPage = new LoanRegistrationPage();
        _loanRegistrationPage.Show();
    }
}

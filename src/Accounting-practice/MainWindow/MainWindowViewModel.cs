using Accounting_practice.Registration;
using Accounting_practice.ViewModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting_practice.MainWindow;

public partial class MainWindowViewModel : Window
{

    private LoanRegistrationPage? _loanRegistrationPage;

    public DependencyObject? View { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void OnViewLoaded()
    {
    }

    public void OnViewUnloaded()
    {
    }

    [RelayCommand]
    private void GoToLoanRegistrationPage()
    {
        _loanRegistrationPage = new LoanRegistrationPage();
        _loanRegistrationPage.Show();
    }
}

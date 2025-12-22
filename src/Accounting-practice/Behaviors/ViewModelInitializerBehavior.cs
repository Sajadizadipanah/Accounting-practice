using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Accounting_practice.ViewModel;
using HandyControl.Interactivity;

namespace Accounting_practice.Behaviors;

public class ViewModelInitializerBehavior : Behavior<FrameworkElement>
{
    public ViewModelInitializerBehavior() : base()
    {

    }

    public static readonly DependencyProperty DataContextSourceElementProperty = DependencyProperty.Register(
        nameof(DataContextSourceElement), typeof(FrameworkElement), typeof(ViewModelInitializerBehavior), new PropertyMetadata(default(FrameworkElement?), PropertyChangedCallback));

    public FrameworkElement? DataContextSourceElement
    {
        get
        {
            return (FrameworkElement?)GetValue(DataContextSourceElementProperty);
        }
        set
        {
            SetValue(DataContextSourceElementProperty, value);
        }
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ViewModelInitializerBehavior behavior)
        {


            if (e.Property == DataContextSourceElementProperty)
            {
                if (behavior.UseAnotherDataContextAsViewModel)
                    behavior.OnDataContextSourceChanged();
            }
            else if (e.Property == UseAnotherDataContextAsViewModelProperty)
            {
                if (behavior.UseAnotherDataContextAsViewModel)
                    behavior.OnDataContextSourceChanged();
            }
        }
    }

    public static readonly DependencyProperty UseAnotherDataContextAsViewModelProperty = DependencyProperty.Register(
        nameof(UseAnotherDataContextAsViewModel), typeof(bool), typeof(ViewModelInitializerBehavior), new PropertyMetadata(false, PropertyChangedCallback));

    public bool UseAnotherDataContextAsViewModel
    {
        get
        {
            return (bool)GetValue(UseAnotherDataContextAsViewModelProperty);
        }
        set
        {
            SetValue(UseAnotherDataContextAsViewModelProperty, value);
        }
    }


    private bool _isAttached = false;
    private FrameworkElement? _associatedObject = null;
    private object? _viewModel = null;

    public void OnDataContextSourceChanged()
    {
        if (_associatedObject != null && DataContextSourceElement != null)
        {
            _associatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
            DataContextSourceElement.DataContextChanged += AssociatedObject_DataContextChanged;
            if (DataContextSourceElement.DataContext != null)
            {
                _viewModel = DataContextSourceElement.DataContext;
                SetViewToViewModel(_associatedObject);
            }
        }
    }

    private void OnAssociatedObjectChanged(FrameworkElement? view)
    {
        if (_associatedObject != null)
        {
            _associatedObject.Loaded -= AssociatedObject_Loaded;
            _associatedObject.Unloaded -= AssociatedObject_Unloaded;
            if (DataContextSourceElement == null)
            {
                _associatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
            }
            else
            {
                DataContextSourceElement.DataContextChanged -= AssociatedObject_DataContextChanged;
            }
        }
        _associatedObject = view;

        if (_associatedObject != null)
        {

            _viewModel = _associatedObject.DataContext;


            SetViewToViewModel(_associatedObject);
            _associatedObject.Loaded += AssociatedObject_Loaded;
            _associatedObject.Unloaded += AssociatedObject_Unloaded;
            if (DataContextSourceElement == null)
            {
                _associatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
            }

        }
    }
    protected override void OnAttached()
    {
        OnAssociatedObjectChanged(this.AssociatedObject);
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        if (_viewModel != null)
        {
            SetViewToViewModel(_associatedObject);
            
            if (_viewModel is IViewModelWithView vmwv)
            {
                vmwv.OnViewLoaded();
            }
        }
    }

    private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
    {
        if (_viewModel is IViewModelWithView vmwv)
        {
            vmwv.OnViewUnloaded();
            vmwv.View = null;
        }
    }

    private void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (_associatedObject != null)
        {
            if (UseAnotherDataContextAsViewModel && DataContextSourceElement != null)
            {
                _viewModel = DataContextSourceElement.DataContext;
                SetViewToViewModel(_associatedObject);


            }
            else
            {

                _viewModel = _associatedObject.DataContext;
                SetViewToViewModel(_associatedObject);
            }

            if (_associatedObject.IsLoaded)
            {
                if (_viewModel is IViewModelWithView vmwv)
                {
                    vmwv.OnViewLoaded();
                }
            }
        }

    }

    protected override void OnDetaching()
    {
        OnAssociatedObjectChanged(null);
        base.OnDetaching();
    }

    //private ISnackbarService? ResolveSnackbarService()
    //{
    //    return AppContext.Current.ServiceProvider.GetService<ISnackbarService>();
    //}
    private void SetViewToViewModel(FrameworkElement? view)
    {
        if (_viewModel is IViewModelWithView viewModel)
        {
            _viewModel = viewModel;
            viewModel.View = view;
        }
    }
}
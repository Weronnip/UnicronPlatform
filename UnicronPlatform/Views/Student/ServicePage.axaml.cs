using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Student;

public partial class ServicePage : ReactiveUserControl<ServicePageViewModel>
{
    public ServicePage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });
    }
}
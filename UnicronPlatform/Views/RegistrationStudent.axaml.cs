using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views;

public partial class RegistrationStudent : ReactiveUserControl<RegistrationUserViewModel>
{
    public RegistrationStudent()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views;

public partial class RegistrationInstructor : ReactiveUserControl<RegistrationUserViewModel>
{
    public RegistrationInstructor()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
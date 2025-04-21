using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Instructor;

public partial class SettingPage : ReactiveUserControl<ISettingPageViewModel>
{
    public SettingPage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Instructor;

public partial class ISettingPage : ReactiveUserControl<ISettingPageViewModel>
{
    public ISettingPage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
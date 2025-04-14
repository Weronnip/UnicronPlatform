using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Instructor;

public partial class IProfilePage : ReactiveUserControl<IProfilePageViewModel>
{
    public IProfilePage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
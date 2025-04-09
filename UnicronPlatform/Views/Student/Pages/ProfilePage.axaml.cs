using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Student.Pages;

public partial class ProfilePage : ReactiveUserControl<ProfilePageViewModel>
{
    public ProfilePage()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        { });
    }
}
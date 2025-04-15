using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace UnicronPlatform.Views.Instructor.Components;

public partial class CreateCourse : ReactiveUserControl<ViewModels.CreateCourse>
{
    public CreateCourse()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
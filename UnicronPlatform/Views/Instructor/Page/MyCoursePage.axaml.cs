using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Instructor.Page;

public partial class MyCoursePage : ReactiveUserControl<MyCoursePageViewModel>
{
    public MyCoursePage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });

    }
}
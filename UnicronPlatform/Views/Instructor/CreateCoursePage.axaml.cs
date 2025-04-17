using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Instructor;

public partial class CreateCoursePage : ReactiveUserControl<CreateCoursePageViewModel>
{
    public CreateCoursePage()
    {
        InitializeComponent();
        this.WhenActivated(disposable => { });
    }
}
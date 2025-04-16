using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;
using System;

namespace UnicronPlatform.Views.Instructor
{
    public partial class ManagementCoursePage : ReactiveUserControl<ManagementCoursePageViewModel>
    {
        public ManagementCoursePage()
        {
            InitializeComponent();
            this.WhenActivated(disposables => { });
        }
    }
}
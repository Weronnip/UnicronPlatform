using System;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using UnicronPlatform.ViewModels;
using ReactiveUI;

namespace UnicronPlatform.Views.Instructor
{
    public partial class MyCoursePage : ReactiveUserControl<MyCoursePageViewModel>
    {
        public MyCoursePage()
        {
            InitializeComponent();
            this.WhenActivated(disposable => { });
        }
    }
}
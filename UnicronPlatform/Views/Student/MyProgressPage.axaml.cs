using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views.Student
{
    public partial class MyProgressPage : ReactiveUserControl<MyProgressPageViewModel>
    {
        public MyProgressPage()
        {
            InitializeComponent();
            this.WhenActivated(disposable => { });
        }
    }
}
using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;
using UnicronPlatform.ViewModels;
using UnicronPlatform.ViewModels;
using UnicronPlatform.Views;
using UnicronPlatform.Views.Instructor;
using UnicronPlatform.Views.Instructor.Components;
using UnicronPlatform.Views.Instructor.Page;
using UnicronPlatform.Views.Student;
using SettingPage = UnicronPlatform.Views.Student.SettingPage;

namespace UnicronPlatform
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; set; } = null!;

        public override void Initialize() =>
            AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Services.GetRequiredService<MainWindowViewModel>()
                };
            }
            // Студенты
            Locator.CurrentMutable.Register(() => new ProfilePage(), typeof(IViewFor<ProfilePageViewModel>));
            Locator.CurrentMutable.Register(() => new SettingPage(), typeof(IViewFor<SettingPageViewModel>));
            Locator.CurrentMutable.Register(() => new ServicePage(), typeof(IViewFor<ServicePageViewModel>));
            Locator.CurrentMutable.Register(() => new MyProgressPage(), typeof(IViewFor<MyProgressPageViewModel>));

            // Преподаватели
            Locator.CurrentMutable.Register(() => new IProfilePage(), typeof(IViewFor<IProfilePageViewModel>));
            Locator.CurrentMutable.Register(() => new ManagementCoursePage(), 
                typeof(IViewFor<ManagementCoursePageViewModel>));
            Locator.CurrentMutable.Register(() => new CreateCoursePage(), 
                typeof(IViewFor<CreateCoursePageViewModel>));
            Locator.CurrentMutable.Register(() => new MyCoursePage(), typeof(IViewFor<MyCoursePageViewModel>));
            base.OnFrameworkInitializationCompleted();
        }
    }
}
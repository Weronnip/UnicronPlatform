using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using UnicronPlatform.ViewModels;
using UnicronPlatform.Views;
using UnicronPlatform.Views.Student;

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
            Locator.CurrentMutable.Register(() => new ProfilePage(), typeof(IViewFor<ProfilePageViewModel>));
            base.OnFrameworkInitializationCompleted();
        }
    }
}
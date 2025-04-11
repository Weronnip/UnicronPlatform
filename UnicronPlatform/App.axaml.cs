using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UnicronPlatform.ViewModels;
using UnicronPlatform.Views;

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
            base.OnFrameworkInitializationCompleted();
        }
    }
}
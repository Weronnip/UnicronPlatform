using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UnicronPlatform.Data;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // Создаём DI-хост
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(
                        "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                        new MySqlServerVersion(new Version(8, 0, 27))
                    )
                );
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();

        App.Services = host.Services;
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
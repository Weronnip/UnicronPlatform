using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
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
        var services = new ServiceCollection();

        // 2. Регистрируем AppDbContext с настройкой MySQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                new MySqlServerVersion(new Version(8, 0, 27))
            ));

        Services = services.BuildServiceProvider();

        var dbContext = Services.GetRequiredService<AppDbContext>();

        var currentUser = dbContext.Users.FirstOrDefault();
        if (currentUser == null)
        {
            throw new Exception("Текущий пользователь не найден в базе данных.");
        }

        var shellVm = new ShellViewModel(dbContext, currentUser);

        var homePage = new HomePage(dbContext, currentUser)
        {
            ViewModel = shellVm
        };

        desktop.MainWindow = homePage;
    }

    base.OnFrameworkInitializationCompleted();
}

    }
}
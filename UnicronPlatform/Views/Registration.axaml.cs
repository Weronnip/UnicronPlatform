using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace UnicronPlatform.Views
{
    public partial class Registration : ReactiveWindow<RegistrationUserViewModel>
    {
        public Registration()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
            );
            var dbContext = new AppDbContext(optionsBuilder.Options);

            DataContext = new RegistrationUserViewModel(dbContext);
        }
    }
}
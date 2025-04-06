using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using UnicronPlatform.Data;

namespace UnicronPlatform.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Interaction<ViewModelBase, ViewModelBase?> ShowDialog { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public AppDbContext _dbContext;

        public MainWindowViewModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
            );

            _dbContext = new AppDbContext(optionsBuilder.Options);

            ShowDialog = new Interaction<ViewModelBase, ViewModelBase?>();

            RegisterCommand = ReactiveCommand.CreateFromTask(() =>
                OpenDialog(new RegistrationUserViewModel(_dbContext)));

            LoginCommand = ReactiveCommand.CreateFromTask(() =>
                OpenDialog(new LoginUserViewModel(_dbContext)));
        }

        private async Task OpenDialog(ViewModelBase viewModel)
        {
            var result = await ShowDialog.Handle(viewModel);
        }
    }
}
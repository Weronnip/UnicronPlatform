using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using UnicronPlatform.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using UnicronPlatform.Models;
using UnicronPlatform.Views;
using UnicronPlatform.Views.Instructor;
using UnicronPlatform.Views.Student;

namespace UnicronPlatform.ViewModels
{
    public class LoginUserViewModel : ViewModelBase
    {
        private string _email;
        public string email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        
        private string _password;
        public string password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        
        public ICommand LoginCommand { get; }
        
        private readonly AppDbContext _dbContext;

        public LoginUserViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            LoginCommand = ReactiveCommand.Create(ExecuteLogin);
        }
        
        private void ExecuteLogin()
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.email == email && u.password == password);

            if (user != null)
            {
                if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    Window homeWindow;

                    var roleID = _dbContext.Role
                        .Where(r => r.role_id == user.role_id)
                        .Select(r => r.role_id)
                        .FirstOrDefault();

                    switch (roleID)
                    {
                        case 1:
                            homeWindow = new HomePage
                            {
                                DataContext = new HomePageViewModel(user)
                            };
                            break;
                        
                        case 2:
                            homeWindow = new HomePageInstructor
                            {
                                DataContext = new IHomePageViewModel(user)
                            };
                            break;
                        default:
                            homeWindow = new MainWindow();
                            break;
                    }

                    homeWindow.WindowState = WindowState.Maximized;
                    homeWindow.Show();

                    foreach (var window in desktop.Windows.ToList())
                    {
                        if (window != homeWindow)
                            window.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверный email или пароль.");
            }
        }


    }
}
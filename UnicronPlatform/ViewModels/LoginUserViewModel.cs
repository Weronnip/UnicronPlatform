using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using UnicronPlatform.Data;
using System.Linq;
using UnicronPlatform.Models;
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
                Console.WriteLine($"Успешный вход: {user.first_name} {user.last_name}");
            }
            else
            {
                Console.WriteLine("Неверный email или пароль.");
            }
        }
    }
}
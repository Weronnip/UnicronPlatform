using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class RegistrationUserViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;
        
        private string _firstName;
        public string first_name
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }
        
        private string _lastName;
        public string last_name
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }
        
        private string _email;
        public string email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        
        private string _phone;
        public string phone
        {
            get => _phone;
            set => this.RaiseAndSetIfChanged(ref _phone, value);
        }
        
        private string _password;
        public string password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        
        public ICommand RegisterCommand { get; }
        
        public RegistrationUserViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            RegisterCommand = ReactiveCommand.CreateFromTask(ExecuteRegisterAsync);
        }
        
        private async Task ExecuteRegisterAsync()
        {
            try
            {
                var newUser = new Users
                {
                    first_name = first_name,
                    last_name = last_name,
                    email = email,
                    phone = phone,
                    role_id = 1,
                    avatar = "../Assets/default_avatar.png",
                    password = password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine($"Пользователь успешно зарегистрирован: {first_name} {last_name}, Email: {email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка регистрации: {ex.Message}");
            }
        }
    }
}

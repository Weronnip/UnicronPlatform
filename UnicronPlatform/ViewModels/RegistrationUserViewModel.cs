using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.Views;

namespace UnicronPlatform.ViewModels
{
    public class RegistrationUserViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;

        private int _user_id;
        private int user_id
        {
            get => _user_id;
            set => this.RaiseAndSetIfChanged(ref _user_id, value);
        }
        
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
        
        private string _bio;
        public string bio
        {
            get => _bio;
            set => this.RaiseAndSetIfChanged(ref _bio, value);
        }
        
        private DateTimeOffset? _experience;
        public DateTimeOffset? experience
        {
            get => _experience;
            set => this.RaiseAndSetIfChanged(ref _experience, value);
        }

        private string _specialization;
        public string specialization
        {
            get => _specialization;
            set => this.RaiseAndSetIfChanged(ref _specialization, value);
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
        
        private string _selectedAccountType;
        public string SelectedAccountType
        {
            get => _selectedAccountType;
            set => this.RaiseAndSetIfChanged(ref _selectedAccountType, value);
        }
        
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }
        
        public ICommand RegisterStudentsCommand { get; }
        public ICommand RegisterInstructorCommand { get; }
        public ICommand SelectAccountTypeCommand { get; }
        
        public RegistrationUserViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
            RegisterStudentsCommand = ReactiveCommand.CreateFromTask(ExecuteRegisterStudentAsync);
            RegisterInstructorCommand = ReactiveCommand.CreateFromTask(ExecuteRegisterInstructorAsync);
            
            SelectAccountTypeCommand = ReactiveCommand.Create<string>(accountType =>
            {
                SelectedAccountType = accountType;
                if (SelectedAccountType == "Student")
                {
                    CurrentView = new RegistrationStudent(); 
                }
                else if (SelectedAccountType == "Instructor")
                {
                    CurrentView = new RegistrationInstructor();
                }
            });

            CurrentView = new RegistrationStudent();
        }
        
        private async Task ExecuteRegisterStudentAsync()
        {
            try
            {
                var newUser = new Users
                {
                    first_name = first_name,
                    last_name = last_name,
                    user_id = user_id,
                    email = email,
                    phone = phone,
                    role_id = 1,
                    balance = 0,
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
                Console.WriteLine($"Ошибка регистрации студента: {ex.Message}");
            }
        }

        private async Task ExecuteRegisterInstructorAsync()
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new Users
                    {
                        first_name = first_name,
                        last_name = last_name,
                        email = email,
                        phone = phone,
                        role_id = 2,
                        balance = 0,
                        password = password,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };

                    _dbContext.Users.Add(newUser);
                    await _dbContext.SaveChangesAsync();

                    var newInstructor = new Instructor
                    {
                        first_name = first_name,
                        last_name = last_name,
                        user_id = user_id,
                        email = email,
                        role_id = 2,
                        bio = bio,
                        experience = experience,
                        specialization = specialization,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };

                    _dbContext.Instructor.Add(newInstructor);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    Console.WriteLine($"Инструктор успешно зарегистрирован: {first_name} {last_name}, Email: {email}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Ошибка регистрации инструктора: {ex.Message}");
                }
            }
        }
    }
}

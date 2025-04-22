using System;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using UnicronPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace UnicronPlatform.ViewModels
{
    public class SettingPageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Настройки";
        public IScreen? HostScreen { get; }
        private readonly AppDbContext _dbContext;

        private Users _user;

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => this.RaiseAndSetIfChanged(ref _phone, value);
        }

        private DateTimeOffset? _birthday;
        public DateTimeOffset? Birthday
        {
            get => _birthday;
            set => this.RaiseAndSetIfChanged(ref _birthday, value);
        }

        private string? _avatar;
        public string? Avatar
        {
            get => _avatar;
            set => this.RaiseAndSetIfChanged(ref _avatar, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public SettingPageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _user = user;

            _firstName = _user.first_name;
            _lastName = _user.last_name;
            _email = _user.email;
            _phone = _user.phone;
            _birthday = _user.birth_date;
            _avatar = _user.avatar;
            _password = _user.password;

            SaveCommand = ReactiveCommand.Create(SaveSettings);
        }


        private void SaveSettings()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;", 
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;"))
                .Options;
            using (var context = new AppDbContext(options))
            {
                var user_id = context.Users.FirstOrDefault(u => u.user_id == _user.user_id);
                if (user_id != null)
                {
                    user_id.first_name = FirstName;
                    user_id.last_name = LastName;
                    user_id.email = Email;
                    user_id.phone = Phone;
                    user_id.birth_date = Birthday;
                    user_id.avatar = Avatar;
                    context.SaveChanges();
                }
            }
            Debug.WriteLine("Настройки сохранены");
        }
    }
}
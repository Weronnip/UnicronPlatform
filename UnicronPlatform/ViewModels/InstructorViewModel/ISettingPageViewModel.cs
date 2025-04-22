using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;
using UnicronPlatform.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace UnicronPlatform.ViewModels
{
    public class ISettingPageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Настройки";
        public IScreen? HostScreen { get; }
        private readonly Users _user;

        private byte[]? _avatar;
        public byte[]? Avatar
        {
            get => _avatar;
            set => this.RaiseAndSetIfChanged(ref _avatar, value);
        }

        public Bitmap AvatarImage
        {
            get
            {
                var data = Avatar;
                if (data != null && data.Length > 0)
                    return new Bitmap(new MemoryStream(data));
                return new Bitmap("avares://UnicronPlatform/Assets/avatar.jpg");
            }
        }

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

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public ISettingPageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _user = user;

            _firstName = user.first_name;
            _lastName = user.last_name;
            _email = user.email;
            _phone = user.phone;
            _birthday = user.birth_date;
            _password = user.password;

            this.WhenAnyValue(x => x.Avatar)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(AvatarImage)));

            SaveCommand = ReactiveCommand.Create(SaveSettings);
        }

        private void SaveSettings()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(
                    "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
                )
                .Options;

            using var context = new AppDbContext(options);
            var entity = context.Users.SingleOrDefault(u => u.user_id == _user.user_id);
            if (entity != null)
            {
                entity.first_name = FirstName;
                entity.last_name = LastName;
                entity.email = Email;
                entity.phone = Phone;
                entity.birth_date = Birthday;
                entity.password = Password;

                context.SaveChanges();
            }
            Debug.WriteLine("Настройки сохранены");
        }
    }
}
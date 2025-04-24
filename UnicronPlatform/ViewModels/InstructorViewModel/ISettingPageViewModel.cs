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
        private readonly Instructor _instr;

        private string? _avatar;
        public string Avatar
        {
            get => _avatar;
            set => this.RaiseAndSetIfChanged(ref _avatar, value);
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

        private string _bio;
        public string Bio
        {
            get => _bio;
            set => this.RaiseAndSetIfChanged(ref _bio, value);
        }

        private string _socialLinkWa;
        public string SocialLinkWA
        {
            get => _socialLinkWa;
            set => this.RaiseAndSetIfChanged(ref _socialLinkWa, value);
        }

        private string _socialLinkVk;
        public string SocialLinkVK
        {
            get => _socialLinkVk;
            set => this.RaiseAndSetIfChanged(ref _socialLinkVk, value);
        }

        private string _socialLinkTg;
        public string SocialLinkTG
        {
            get => _socialLinkTg;
            set => this.RaiseAndSetIfChanged(ref _socialLinkTg, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public ISettingPageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _user = user;

            // Инициализируем из Users
            _firstName = _user.first_name;
            _lastName = _user.last_name;
            _email = _user.email;
            _phone = _user.phone;
            _birthday = _user.birth_date;
            _password = _user.password;
            _avatar = _user.avatar;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(
                    "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
                )
                .Options;

            using (var context = new AppDbContext(options))
            {
                _instr = context.Instructor
                    .SingleOrDefault(i => i.user_id == _user.user_id)
                    ?? new Instructor { user_id = _user.user_id };

                _bio = _instr.bio;
                _socialLinkWa = _instr.social_link_wa;
                _socialLinkVk = _instr.social_link_vk;
                _socialLinkTg = _instr.social_link_tg;
            }

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

            var userEntity = context.Users.SingleOrDefault(u => u.user_id == _user.user_id);
            if (userEntity != null)
            {
                userEntity.first_name = FirstName;
                userEntity.last_name = LastName;
                userEntity.email = Email;
                userEntity.phone = Phone;
                userEntity.birth_date = Birthday;
                userEntity.password = Password;
                userEntity.avatar = Avatar;
            }

            var instrEntity = context.Instructor.SingleOrDefault(i => i.user_id == _user.user_id);
            if (instrEntity != null)
            {
                instrEntity.first_name = FirstName;
                instrEntity.last_name = LastName;
                instrEntity.bio = Bio;
                instrEntity.social_link_wa = SocialLinkWA;
                instrEntity.social_link_vk = SocialLinkVK;
                instrEntity.social_link_tg = SocialLinkTG;
            }
            else
            {
                instrEntity = new Instructor
                {
                    user_id = _user.user_id,
                    first_name = FirstName,
                    last_name = LastName,
                    bio = Bio,
                    social_link_wa = SocialLinkWA,
                    social_link_vk = SocialLinkVK,
                    social_link_tg = SocialLinkTG
                };
                context.Instructor.Add(instrEntity);
            }

            context.SaveChanges();

            _user.first_name = FirstName;
            _user.last_name = LastName;
            _user.email = Email;
            _user.phone = Phone;
            _user.birth_date = Birthday;
            _user.password = Password;
            _user.avatar = Avatar;

            _instr.first_name = FirstName;
            _instr.last_name = LastName;
            _instr.bio = Bio;
            _instr.social_link_wa = SocialLinkWA;
            _instr.social_link_vk = SocialLinkVK;
            _instr.social_link_tg = SocialLinkTG;

            Debug.WriteLine("Настройки сохранены и кэш обновлён");
        }
    }
}

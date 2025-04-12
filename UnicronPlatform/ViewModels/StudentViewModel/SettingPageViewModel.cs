using System;
using Avalonia.Controls;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;
using ReactiveUI;

namespace UnicronPlatform.ViewModels
{
    public class SettingPageViewModel : ReactiveObject,  IRoutableViewModel
    {
        public string? UrlPathSegment => "Настройки";
        public IScreen? HostScreen { get; }
        
        private Users _user;

        private Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public SettingPageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            this.user = user;
        }
        
        public string first_name => user.first_name;
        public string last_name => user.last_name; 
        public string email => user.email;
        public string phone => user.phone;
        public DateTime? birthday => user.birth_date;
        public string? avatar => user.avatar;
        public string password => user.password;

    }
}
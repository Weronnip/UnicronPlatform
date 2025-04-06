using System;
using ReactiveUI;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ProfilePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "profile";
        public IScreen HostScreen { get; set; }
        
        private Users _user;

        public Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public ProfilePageViewModel(IScreen hostScreen, Users users)
        {
            HostScreen = hostScreen ?? throw new ArgumentException(nameof(hostScreen));
            _user = users; 

            HostScreen.Router.Navigate.Execute(this);
        }

        public string avatar => string.IsNullOrEmpty(user.avatar) ? "../Assets/avatar.png" : user.avatar;
        
        public string full_name => user.first_name + " " + user.last_name;
        public string email => user.email;
        public string phone => user.phone;
    }
}
using System;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class IProfilePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => full_name;
        public IScreen? HostScreen { get; }

        private Users _user;

        private Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public IProfilePageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            this.user = user;
        }

        public string avatar => string.IsNullOrEmpty(user.avatar)
            ? "avares://UnicronPlatform/Assets/avatar.jpg"
            : user.avatar;

        public string full_name => $"{user.first_name} {user.last_name}";
        public string email => user.email;
        public string phone => user.phone;
        public string balance => $"{(int)user.balance!}$";
    }
}
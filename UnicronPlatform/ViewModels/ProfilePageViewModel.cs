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
            HostScreen = hostScreen ?? new DummyScreen();
            _user = users ?? throw new ArgumentNullException(nameof(users));
        }

        public string avatar => string.IsNullOrEmpty(user.avatar)
            ? "avares://UnicronPlatform/Assets/avatar.png"
            : user.avatar;

        public string full_name => $"{user.first_name} {user.last_name}";
        public string email => user.email;
        public string phone => user.phone;
    }

    public class DummyScreen : IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
    }
}
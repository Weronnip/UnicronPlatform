using System;
using ReactiveUI;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class SettingsPageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "settings";
        public IScreen HostScreen { get; }

        private Users _user;
        public Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public SettingsPageViewModel(IScreen hostScreen, Users user)
        {
            HostScreen = hostScreen;
            this.user = user;
        }
    }
}
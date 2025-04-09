// File: ViewModels/SettingsPageViewModel.cs
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class SettingsPageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "settings";
        public IScreen HostScreen { get; }

        private readonly AppDbContext _db;
        private Users _user;

        public Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public SettingsPageViewModel(IScreen hostScreen, Users user, AppDbContext dbContext)
        {
            HostScreen = hostScreen;
            _user = user;
            _db = dbContext;
        }
    }
}
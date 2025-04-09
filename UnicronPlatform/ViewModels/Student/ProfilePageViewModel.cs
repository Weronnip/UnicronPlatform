// File: ViewModels/ProfilePageViewModel.cs
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ProfilePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "profile";
        public IScreen HostScreen { get; }

        private Users _user;
        public Users user
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        private string _role_name = "Загрузка...";
        public string role_name
        {
            get => _role_name;
            set => this.RaiseAndSetIfChanged(ref _role_name, value);
        }
        public ProfilePageViewModel(IScreen hostScreen, Users user, AppDbContext dbContext)
        {
            HostScreen = hostScreen ?? throw new ArgumentNullException(nameof(hostScreen));
            _user = user ?? throw new ArgumentNullException(nameof(user));

            LoadRoleNameAsync(dbContext);
        }

        private async void LoadRoleNameAsync(AppDbContext dbContext)
        {
            if (user.role_id == null)
            {
                role_name = "Роль не указана";
                return;
            }
            
            var result = await dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.user_id == user.user_id);

            role_name = (result?.Role?.name_role) ?? "Роль не найдена";
        }
        
        public string avatar => string.IsNullOrEmpty(user.avatar) 
            ? "../../Assets/default_avatart.jpg"
            : user.avatar;
        public string full_name => $"{user.first_name} {user.last_name}";
        public string email => user.email;
        public string phone => user.phone;
    }
}

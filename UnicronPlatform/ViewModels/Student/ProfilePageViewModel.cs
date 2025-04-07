using System;
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
        public IScreen HostScreen { get; set; }

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

        public ProfilePageViewModel(IScreen hostScreen, Users users, AppDbContext dbContext)
        {
            HostScreen = hostScreen ?? new DummyScreen();
            _user = users ?? throw new ArgumentNullException(nameof(users));

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

            if (result != null && result.Role != null)
            {
                role_name = result.Role.name_role;
            }
            else
            {
                role_name = "Роль не найдена";
            }
        }



        public string avatar => string.IsNullOrEmpty(user.avatar) 
            ? "avares://UnicronPlatform/Assets/default_avatart.jpg"
            : user.avatar;

        public string full_name => $"{user.first_name} {user.last_name}";
        public string email => user.email;
        public string phone => user.phone;

        public class DummyScreen : IScreen
        {
            public RoutingState Router { get; } = new RoutingState();
        }
    }
}

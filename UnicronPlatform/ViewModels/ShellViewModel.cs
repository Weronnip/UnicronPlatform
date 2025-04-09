using System;
using System.Reactive;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ShellViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new();
        
        // Пример команды для перехода на страницу профиля.
        public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToSettings { get; }

        private readonly AppDbContext _db;
        private readonly Users _user;

        public ShellViewModel(AppDbContext dbContext, Users user)
        {
            _db = dbContext;
            _user = user;

            GoToProfile = ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new ProfilePageViewModel(this, _user, _db)));
            
            GoToSettings = ReactiveCommand.CreateFromObservable( () => 
                Router.Navigate.Execute(new SettingsPageViewModel(this, _user, _db)));
            
            // Переход по умолчанию на страницу профиля.
            GoToProfile.Execute().Subscribe();
        }
    }
}
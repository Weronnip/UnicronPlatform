using System;
using System.Reactive;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.Views.Student.Pages;

namespace UnicronPlatform.ViewModels
{
    public class HomePageViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        
        public HomePageViewModel(AppDbContext dbContext, Users currentUser)
        {
            var _currentUser = currentUser;

            NavigateToProfileCommand = ReactiveCommand.Create(() =>
            {
                var profileViewModel = new ProfilePageViewModel(this, _currentUser, dbContext);
                Router.Navigate.Execute(profileViewModel).Subscribe();
            });

            NavigateToSettingsCommand = ReactiveCommand.Create(() =>
            {
                var settingsViewModel = new SettingsPageViewModel(this, _currentUser);
                Router.Navigate.Execute(settingsViewModel).Subscribe();
            });
        }


        public ReactiveCommand<Unit, Unit> NavigateToProfileCommand { get; }
        public ReactiveCommand<Unit, Unit> NavigateToSettingsCommand { get; }
    }
}
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.Views;
using UnicronPlatform.Views.Instructor;
using UnicronPlatform.Views.Student;

namespace UnicronPlatform.ViewModels
{
    public class IHomePageViewModel : ReactiveObject, IScreen
    {
        private Users _user;
        
        private Users User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }
        
        private Courses _courses;
        private Courses Course
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }
        
        
        public RoutingState Router { get; } = new RoutingState();
        
        public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToSettings { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToShop { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToManagementCourse { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToAnalytics { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToSuppots { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;

        public IHomePageViewModel(Users user)
        {
            User = user;
            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            var initialViewModel = new IProfilePageViewModel(this, User);
            Router.Navigate.Execute(initialViewModel)
                .Subscribe(
                    _ => Console.WriteLine("Initial navigation to ProfilePageViewModel succeeded."),
                    ex => Console.WriteLine("Navigation error: " + ex.Message)
                );

            GoToProfile = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new IProfilePageViewModel(this, User);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            GoToSettings = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new SettingPageViewModel(this, User);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });
            
            GoToManagementCourse = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new ManagementCoursePageViewModel(this);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            LogoutCommand = ReactiveCommand.Create(ExecuteLogout);
        }

        private void ExecuteLogout()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime 
                is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
        
                desktop.MainWindow = mainWindow;
        
                mainWindow.Show();
                mainWindow.WindowState = WindowState.Normal;
        
                var homeWindow = desktop.Windows.OfType<HomePageInstructor>().FirstOrDefault();
                if(homeWindow != null)
                {
                    homeWindow.Close();
                }
            }
        }
    }
}

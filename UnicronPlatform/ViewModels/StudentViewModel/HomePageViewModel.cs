using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.ViewModels;
using UnicronPlatform.Views;
using UnicronPlatform.Views.Student;

namespace UnicronPlatform.ViewModels
{
    public class HomePageViewModel : ReactiveObject, IScreen
    {
        private Users _user;
        
        private Users User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }
        
        private Plans _plan;
        private Plans Plan
        {
            get => _plan;
            set => this.RaiseAndSetIfChanged(ref _plan, value);
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
        public ReactiveCommand<Unit, IRoutableViewModel> GoToService { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToMyProgress { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }


        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;

        public HomePageViewModel(Users user)
        {
            User = user;
            Plan = _plan;
            Course = _courses;
            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            var initialViewModel = new ProfilePageViewModel(this);
            Router.Navigate.Execute(initialViewModel)
                .Subscribe(
                    _ => Console.WriteLine("Initial navigation to ProfilePageViewModel succeeded."),
                    ex => Console.WriteLine("Navigation error: " + ex.Message)
                );

            GoToProfile = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new ProfilePageViewModel(this);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            GoToSettings = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new SettingPageViewModel(this, User);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            GoToService = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new ServicePageViewModel(this);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            GoToMyProgress = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new MyProgressPageViewModel(this);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            LogoutCommand = ReactiveCommand.Create(ExecuteLogout);

            Console.WriteLine($"Initial CurrentViewModel: {CurrentViewModel?.GetType().Name ?? "null"}");
        }
        
        private void ExecuteLogout()
        {
            Splat.Locator.CurrentMutable.UnregisterCurrent<Users>();
            if (Avalonia.Application.Current?.ApplicationLifetime 
                is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
        
                desktop.MainWindow = mainWindow;
        
                mainWindow.Show();
                mainWindow.WindowState = WindowState.Normal;
        
                var homeWindow = desktop.Windows.OfType<HomePage>().FirstOrDefault();
                if(homeWindow != null)
                {
                    homeWindow.Close();
                }
            }
        }
    }
}

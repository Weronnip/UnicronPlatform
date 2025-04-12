using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
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

        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;

        // Конструктор
        public HomePageViewModel(Users user)
        {
            User = user;
            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            var initialViewModel = new ProfilePageViewModel(this, User);
            Router.Navigate.Execute(initialViewModel)
                .Subscribe(
                    _ => Console.WriteLine("Initial navigation to ProfilePageViewModel succeeded."),
                    ex => Console.WriteLine("Navigation error: " + ex.Message)
                );

            GoToProfile = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new ProfilePageViewModel(this, User);
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
                var vm = new ServicePageViewModel(this, Plan, Course);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });

            Console.WriteLine($"Initial CurrentViewModel: {CurrentViewModel?.GetType().Name ?? "null"}");
        }
    }
}

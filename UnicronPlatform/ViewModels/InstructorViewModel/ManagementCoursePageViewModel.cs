using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.ViewModels
{
    public class ManagementCoursePageViewModel : ReactiveObject, IRoutableViewModel, IScreen

    {
        public string? UrlPathSegment => "Панель управления";
        public IScreen? HostScreen { get; }
        private readonly AppDbContext _context;
        
        public  ReactiveCommand<Unit, IRoutableViewModel> GoToCreateCourse { get; }
        public  ReactiveCommand<Unit, IRoutableViewModel> GoToMyCourse { get; }
        public  ReactiveCommand<Unit, IRoutableViewModel> GoToAnalyticCourse { get; }
        public  ReactiveCommand<Unit, IRoutableViewModel> GoToHistoryCourse { get; }
        public  ReactiveCommand<Unit, IRoutableViewModel> GoToEditCourse { get; }

        private Courses _courses;
        public Courses Courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }
        private ReactiveObject _currentContent;
        public ReactiveObject CurrentContent
        {
            get => _currentContent;
            set => this.RaiseAndSetIfChanged(ref _currentContent, value);
        }
        
        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;
        
        public ManagementCoursePageViewModel(IScreen hostScreen)
        {
            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            var initialViewModel = new MyCoursePageViewModel(this, Courses);
            Router.Navigate.Execute(initialViewModel)
                .Subscribe(
                    _ => Console.WriteLine("Initial navigation to ManagementCoursePageViewModel succeeded."),
                    ex => Console.WriteLine("Navigation error: " + ex.Message)
                );
            
            GoToMyCourse = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new MyCoursePageViewModel(this, Courses);
                await Router.Navigate.Execute(vm);
                return (IRoutableViewModel)vm;
            });
        }

        public RoutingState Router { get; }
    }
}
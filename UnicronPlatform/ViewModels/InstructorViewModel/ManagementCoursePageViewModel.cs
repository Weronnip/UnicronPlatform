using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ManagementCoursePageViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public string? UrlPathSegment => "Панель управления";
        public IScreen? HostScreen { get; }
        public RoutingState Router { get; } = new RoutingState();
        private readonly AppDbContext _context;
        private readonly List<Courses> _allCourses = new();
        private Courses _courses;
        public Courses Courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }

        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;

        public ReactiveCommand<Unit, IRoutableViewModel> GoToMyCourse { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToCreateCourse { get; }
        public ReactiveCommand<Unit, Unit> GoToRefreshCourse { get; }


        public ManagementCoursePageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(
                    "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
                )
                .Options;

            _context = new AppDbContext(options); 

            LoadCourseDataAsync();

            GoToMyCourse = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var instructor_id = _allCourses.FirstOrDefault()?.instructor_id ?? 0;
                var vm = new MyCoursePageViewModel(this, _allCourses, instructor_id);
                await Router.Navigate.Execute(vm);
                return vm;
            });

            GoToCreateCourse = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var instructor_id = _allCourses.FirstOrDefault()?.instructor_id ?? 0;
                var vm = new CreateCoursePageViewModel(this, _context, instructor_id);
                await Router.Navigate.Execute(vm);
                return vm;
            });

            GoToRefreshCourse = ReactiveCommand.CreateFromTask(() => LoadCourseDataAsync());
            GoToCreateCourse.Execute().Subscribe();
        }

        private async Task LoadCourseDataAsync()
        {
            try
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(
                        "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                        ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;")
                    )
                    .Options;

                using var db = new AppDbContext(options);
                var coursesList = await db.Courses.ToListAsync();
                _allCourses.Clear();
                _allCourses.AddRange(coursesList);

                _allCourses.Clear();
                _allCourses.AddRange(coursesList);
                var instructorId = _allCourses.FirstOrDefault()?.instructor_id ?? 0;
                var initialViewModel = new MyCoursePageViewModel(this, _allCourses, instructorId);
                await Router.Navigate.Execute(initialViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading courses: " + ex);
            }
        }

    }
}

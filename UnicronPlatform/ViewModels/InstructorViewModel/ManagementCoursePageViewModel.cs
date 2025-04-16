using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Для метода FirstOrDefaultAsync()
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ManagementCoursePageViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        // URL-адрес для навигации
        public string? UrlPathSegment => "Панель управления";

        // Родительский экран для навигации (обычно устанавливается при старте приложения)
        public IScreen? HostScreen { get; }

        // Контейнер навигации
        public RoutingState Router { get; } = new RoutingState();

        private Courses _courses;
        public Courses Courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }

        public IRoutableViewModel? CurrentViewModel =>
            Router.NavigationStack.Count > 0 ? Router.NavigationStack.Last() : null;

        public ReactiveCommand<Unit, IRoutableViewModel> GoToMyCourse { get; }

        public ManagementCoursePageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            this.WhenAnyValue(x => x.Router.NavigationStack.Count)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CurrentViewModel)));

            LoadCourseDataAsync();

            GoToMyCourse = ReactiveCommand.CreateFromTask<Unit, IRoutableViewModel>(async _ =>
            {
                var vm = new MyCoursePageViewModel(this, Courses);
                await Router.Navigate.Execute(vm);
                return vm;
            });
        }

        /// <summary>
        /// Асинхронная загрузка данных из базы.
        /// </summary>
        private async void LoadCourseDataAsync()
        {
            try
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                        ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;"))
                    .Options;
                using (var db = new AppDbContext(options))
                {
                    Courses = await db.Courses.FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading courses: " + ex);
                Courses = new Courses { title = "Нет данных", description = "Нет данных" };
            }
            var initialViewModel = new MyCoursePageViewModel(this, Courses);
            await Router.Navigate.Execute(initialViewModel);
        }
    }
}

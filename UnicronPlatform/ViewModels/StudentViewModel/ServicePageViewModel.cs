using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ServicePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Магазин";
        public IScreen? HostScreen { get; }

        private Plans _plan;
        private Plans plan
        {
            get => _plan;
            set => this.RaiseAndSetIfChanged(ref _plan, value);
        }

        private ObservableCollection<PlanDto> _plansList;
        public ObservableCollection<PlanDto> PlansList
        {
            get => _plansList;
            set => this.RaiseAndSetIfChanged(ref _plansList, value);
        }

        private Courses courses;

        private ObservableCollection<CoursesDto> _coursesList = new();
        public ObservableCollection<CoursesDto> CoursesList
        {
            get => _coursesList;
            set => this.RaiseAndSetIfChanged(ref _coursesList, value);
        }

        private const int pageSize = 2;
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentPage, value);
                LoadCourses();
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set => this.RaiseAndSetIfChanged(ref _totalPages, value);
        }

        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PreviousPageCommand { get; }

        private readonly string connectionString = "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;";

        public ServicePageViewModel(IScreen hostScreen, Plans plan, Courses course)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            this.plan = plan;
            this.courses = course;

            PlansList = new ObservableCollection<PlanDto>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var plans = context.Plans.ToList();
                foreach (var p in plans)
                {
                    PlansList.Add(new PlanDto
                    {
                        Name = p.name,
                        Description = p.description,
                        Price = $"{p.price?.ToString("F2") ?? "0.00"}$",
                        Duration = $"{p.duration} Месяц"
                    });
                }

                var totalCourses = context.Courses.Count();
                TotalPages = (int)Math.Ceiling((double)totalCourses / pageSize);
            }

            NextPageCommand = ReactiveCommand.Create(() =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });

            PreviousPageCommand = ReactiveCommand.Create(() =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });

            LoadCourses();
        }

        private void LoadCourses()
        {
            CoursesList.Clear();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var courses = context.Courses
                    .Include(c => c.Instructor)
                    .OrderBy(c => c.course_id)
                    .Skip((CurrentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                foreach (var c in courses)
                {
                    var author = c.Instructor != null
                        ? $"{c.Instructor.first_name} {c.Instructor.last_name}"
                        : "Неизвестен";

                    CoursesList.Add(new CoursesDto
                    {
                        Title = c.title,
                        Description = c.description,
                        Price = $"{c.price?.ToString("F2") ?? "0.00"}$",
                        Author = author
                    });
                }
            }
        }
    }

    public class PlanDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
    }

    public class CoursesDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string? Author { get; set; }
    }
}

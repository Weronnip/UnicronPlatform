using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class CreateCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Новый курс";
        public IScreen HostScreen { get; }

        private readonly AppDbContext _context;

        private string _title = string.Empty;
        public string title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _description = string.Empty;
        public string description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        private decimal _price;
        public decimal price
        {
            get => _price;
            set => this.RaiseAndSetIfChanged(ref _price, value);
        }

        private int _total_lessons;
        public int total_lessons
        {
            get => _total_lessons;
            set => this.RaiseAndSetIfChanged(ref _total_lessons, value);
        }

        private int _control_point;
        public int control_point
        {
            get => _control_point;
            set => this.RaiseAndSetIfChanged(ref _control_point, value);
        }

        public ReactiveCommand<Unit, Unit> CreateCourseCommand { get; }

        public CreateCoursePageViewModel(IScreen hostScreen, AppDbContext context)
        {
            HostScreen = hostScreen;
            _context = context;

            CreateCourseCommand = ReactiveCommand.CreateFromTask(CreateCourseAsync);

            CreateCourseCommand.ThrownExceptions.Subscribe(ex =>
            {
                Console.WriteLine($"Ошибка при создании курса: {ex.Message}");
            });
        }

        private async Task CreateCourseAsync()
        {
            try
            {
                var newCourse = new Courses
                {
                    title = title,
                    description = description,
                    price = price,
                    total_lessons = total_lessons,
                    control_point = control_point,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Курс успешно создан: \"{title}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось создать курс: {ex.Message}");
            }
        }
    }
}

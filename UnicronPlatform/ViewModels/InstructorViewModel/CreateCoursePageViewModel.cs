using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.Views;

namespace UnicronPlatform.ViewModels
{
    public class CreateCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Новый курс";
        public IScreen HostScreen { get; }

        private readonly AppDbContext _context;
        private readonly int _instructor_id;

        public ObservableCollection<Category> Categories { get; } = new();
        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedCategory, value);
                this.RaisePropertyChanged(nameof(CanCreate));
            }
        }

        public bool CanCreate => SelectedCategory != null
                                 && !string.IsNullOrWhiteSpace(title)
                                 && !string.IsNullOrWhiteSpace(description);

        private string _title = string.Empty;
        public string title
        {
            get => _title;
            set
            {
                this.RaiseAndSetIfChanged(ref _title, value);
                this.RaisePropertyChanged(nameof(CanCreate));
            }
        }

        private string _description = string.Empty;
        public string description
        {
            get => _description;
            set
            {
                this.RaiseAndSetIfChanged(ref _description, value);
                this.RaisePropertyChanged(nameof(CanCreate));
            }
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

            var currentUser = Locator.Current.GetService<Users>()
                ?? throw new InvalidOperationException("Пользователь не найден в Locator");

            if (currentUser.role_id != 2)
                throw new InvalidOperationException("Текущий пользователь не является преподавателем");

            _instructor_id = currentUser.user_id;

            LoadCategories();

            CreateCourseCommand = ReactiveCommand.CreateFromTask(CreateCourseAsync, this.WhenAnyValue(vm => vm.CanCreate));
            CreateCourseCommand.ThrownExceptions.Subscribe(ex =>
            {
                Console.WriteLine($"Ошибка при создании курса: {ex.Message}");
                new NotificationWindow(false).Show();
            });
        }

        private async void LoadCategories()
        {
            var list = await _context.Category.OrderBy(c => c.name).ToListAsync();
            Categories.Clear();
            foreach (var cat in list)
                Categories.Add(cat);
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
                    category_id = SelectedCategory!.category_id,
                    instructor_id = _instructor_id,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                new NotificationWindow(true).Show();
                Console.WriteLine($"Курс успешно создан: \"{title}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось создать курс: {ex.Message}");
            }
        }
    }
}

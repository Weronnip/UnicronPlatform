using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class CreateCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Новый курс";
        public IScreen? HostScreen { get; }

        private readonly AppDbContext _context;
        private Courses _courses;
        
        private string _title;
        public string title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        
        private string _description;
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

        public CreateCoursePageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{

    public class MyCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Мои курсы";
        public IScreen? HostScreen { get; }

        private readonly AppDbContext _context;
        private Courses _courses;
        public Courses courses;
        
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

        public MyCoursePageViewModel(IScreen hostScreen, Courses courses)
        {
            HostScreen = hostScreen;
            this.courses = courses;
        }

        public string title_course => courses.title;
        public string description_course => courses.description;

    }
}
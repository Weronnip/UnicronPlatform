using ReactiveUI;
using Splat;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ServicePageViewModel: ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Услуги";
        public IScreen? HostScreen { get; }

        private Plans _plan;
        private Plans plan
        {
            get => _plan;
            set => this.RaiseAndSetIfChanged(ref _plan, value);
        }
        
        private Courses _courses;
        private Courses courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }

        public ServicePageViewModel(IScreen hostScreen, Plans plan, Courses courses)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            this.plan = plan;
            this.courses = courses;
        }
        
    }
}
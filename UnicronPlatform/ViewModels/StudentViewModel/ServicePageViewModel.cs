using System.Diagnostics;
using ReactiveUI;
using Splat;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ServicePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Услуги";
        public IScreen? HostScreen { get; }

        private Plans _plan;
        public Plans Plan
        {
            get => _plan;
            set => this.RaiseAndSetIfChanged(ref _plan, value);
        }
        
        private Courses _courses;
        public Courses Courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }

        public ServicePageViewModel(IScreen hostScreen, Plans plan, Courses courses)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            Plan = plan;
            Courses = courses;
        }
        
        public string plan_name => Plan.name;
        public string? plan_description => Plan.description;
        public decimal plan_price => (decimal)Plan.price;
        public int plan_duration => Plan.duration;
    }
}
using ReactiveUI;
using Splat;

namespace UnicronPlatform.ViewModels
{
    public class ManagementCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Панель управления";
        public IScreen? HostScreen { get; }

        public ManagementCoursePageViewModel(IHomePageViewModel hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
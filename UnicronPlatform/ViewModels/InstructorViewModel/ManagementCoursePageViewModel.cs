using System;
using ReactiveUI;
using Splat;

namespace UnicronPlatform.ViewModels
{
    public class ManagementCoursePageViewModel : ReactiveObject, IRoutableViewModel, IScreen

    {
        public string? UrlPathSegment => "Панель управления";
        public IScreen? HostScreen { get; }
        private ReactiveObject _currentContent;
        public ReactiveObject CurrentContent
        {
            get => _currentContent;
            set => this.RaiseAndSetIfChanged(ref _currentContent, value);
        }
        
        

        public ManagementCoursePageViewModel(IHomePageViewModel hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            CurrentContent = new CreateCoursePageViewModel(this);
        }

        public RoutingState Router { get; }
    }
}
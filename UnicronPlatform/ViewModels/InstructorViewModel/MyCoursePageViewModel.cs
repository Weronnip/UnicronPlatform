using ReactiveUI;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class MyCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        // URL для данной страницы
        public string? UrlPathSegment => "Мои курсы";

        public IScreen? HostScreen { get; }

        // Храним курс, переданный из родительской VM
        public Courses Courses { get; }

        public MyCoursePageViewModel(IScreen hostScreen, Courses courses)
        {
            HostScreen = hostScreen;
            Courses = courses;
        }

        public string TitleCourse => Courses != null ? Courses.title : "Нет данных";
        public string DescriptionCourse => Courses != null ? Courses.description : "Нет данных";
    }
}
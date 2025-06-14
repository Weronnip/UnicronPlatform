using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class MyCoursePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Мои курсы";
        public IScreen? HostScreen { get; }

        private readonly List<Courses> _myCoursesRaw;
        private readonly ObservableCollection<Courses> _pagedCourses = new();
        public ReadOnlyObservableCollection<Courses> MyCourses { get; }

        private int _currentPage = 1;
        private const int pageSize = 4;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (this.RaiseAndSetIfChanged(ref _currentPage, value) != 0)
                {
                    UpdatePagedCourses();
                }
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)_myCoursesRaw.Count / pageSize);
        public bool CanGoPrev => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotalPages;
        public bool HasCourses => _myCoursesRaw.Any();

        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PrevPageCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenContextMenuCommand { get; }
        public ReactiveCommand<Unit, Unit> ManageLessonsCommand { get; }
        public ReactiveCommand<Unit, Unit> EditCourseCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCourseCommand { get; }

        public MyCoursePageViewModel(IScreen hostScreen, IEnumerable<Courses> allCourses, int instructor_id)
        {
            HostScreen = hostScreen;
            _myCoursesRaw = allCourses
                .Where(c => c.instructor_id == instructor_id)
                .ToList();

            MyCourses = new ReadOnlyObservableCollection<Courses>(_pagedCourses);

            NextPageCommand = ReactiveCommand.Create(() =>
            {
                if (CanGoNext)
                    CurrentPage++;
            });

            PrevPageCommand = ReactiveCommand.Create(() =>
            {
                if (CanGoPrev)
                    CurrentPage--;
            });

            OpenContextMenuCommand = ReactiveCommand.Create(() => { /* Логика для открытия контекстного меню */ });
            ManageLessonsCommand = ReactiveCommand.Create(() => { /* Логика управления уроками */ });
            EditCourseCommand = ReactiveCommand.Create(() => { /* Логика редактирования курса */ });
            DeleteCourseCommand = ReactiveCommand.Create(() => { /* Логика удаления курса */ });

            UpdatePagedCourses();
        }

        private void UpdatePagedCourses()
        {
            _pagedCourses.Clear();

            var items = _myCoursesRaw
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);

            foreach (var course in items)
            {
                _pagedCourses.Add(course);
            }
            this.RaisePropertyChanged(nameof(CanGoPrev));
            this.RaisePropertyChanged(nameof(CanGoNext));
            this.RaisePropertyChanged(nameof(TotalPages));
            this.RaisePropertyChanged(nameof(CurrentPage));
        }
    }
}

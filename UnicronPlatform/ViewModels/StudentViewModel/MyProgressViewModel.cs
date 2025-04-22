using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class MyProgressPageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Мой прогресс";
        public IScreen? HostScreen { get; }
        
        private readonly List<Courses> _allCoursesRaw;
        private readonly string _connectionString =
            "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;";
        private readonly ObservableCollection<Courses> _pagedCourses = new();
        public ReadOnlyObservableCollection<Courses> CurrentCoursesPage { get; }

        private int _currentPage = 1;
        private const int PageSize = 2;

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

        public int TotalPages => (int)Math.Ceiling((double)_allCoursesRaw.Count / PageSize);
        public bool CanGoPrev => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotalPages;
        public bool HasCourses => _allCoursesRaw.Any();

        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PrevPageCommand { get; }

        public MyProgressPageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .Options;
            using var dbContext = new AppDbContext(options);

            var currentUser = Locator.Current.GetService<Users>()
                              ?? throw new InvalidOperationException("Пользователь не найден в Locator");
            int currentUserId = currentUser.user_id;

            _allCoursesRaw = dbContext.UserCourses
                .Include(uc => uc.Courses)
                .Where(uc => uc.user_id == currentUserId)
                .Select(uc => uc.Courses!)
                .ToList();

            CurrentCoursesPage = new ReadOnlyObservableCollection<Courses>(_pagedCourses);

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

            UpdatePagedCourses();
        }

        private void UpdatePagedCourses()
        {
            _pagedCourses.Clear();

            var items = _allCoursesRaw
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

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

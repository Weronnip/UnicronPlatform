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
    public class ProfilePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => FullName;
        public IScreen? HostScreen { get; }

        private readonly Users _user;
        private readonly List<Payments> _allPaymentsRaw;
        private readonly ObservableCollection<Payments> _pagedPayments = new();
        public ReadOnlyObservableCollection<Payments> CurrentPaymentsPage { get; }

        private const int PageSize = 5;
        private int _currentPage = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (this.RaiseAndSetIfChanged(ref _currentPage, value) != 0)
                {
                    UpdatePagedPayments();
                }
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)_allPaymentsRaw.Count / PageSize);
        public bool CanGoPrev => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotalPages;
        public bool HasPayments => _allPaymentsRaw.Any();

        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PrevPageCommand { get; }

        public ProfilePageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _user = Locator.Current.GetService<Users>() 
                    ?? throw new InvalidOperationException("Пользователь не найден в Locator");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                          ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;"))
                .Options;

            using var dbContext = new AppDbContext(options);

            _allPaymentsRaw = dbContext.Payments
                .Include(p => p.Courses)
                .Include(p => p.Plans)
                .Where(p => p.user_id == _user.user_id)
                .OrderByDescending(p => p.created_at)
                .ToList();

            CurrentPaymentsPage = new ReadOnlyObservableCollection<Payments>(_pagedPayments);

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

            UpdatePagedPayments();
        }

        private void UpdatePagedPayments()
        {
            _pagedPayments.Clear();

            var items = _allPaymentsRaw
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

            foreach (var payment in items)
            {
                _pagedPayments.Add(payment);
            }

            this.RaisePropertyChanged(nameof(CanGoPrev));
            this.RaisePropertyChanged(nameof(CanGoNext));
            this.RaisePropertyChanged(nameof(TotalPages));
            this.RaisePropertyChanged(nameof(CurrentPage));
        }

        public string Avatar => string.IsNullOrEmpty(_user.avatar)
            ? "../../Assets/avatar.jpg"
            : _user.avatar;

        public string FullName => $"{_user.first_name} {_user.last_name}";
        public string Email => _user.email;
        public string Phone => _user.phone;
        public string Balance => $"{(int)_user.balance!}$";
    }
}

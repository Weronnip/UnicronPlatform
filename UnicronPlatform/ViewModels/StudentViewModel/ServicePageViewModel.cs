using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.Views;

namespace UnicronPlatform.ViewModels
{
    public class ServicePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Магазин";
        public IScreen? HostScreen { get; }

        private readonly string _connectionString =
            "server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;";

        public ObservableCollection<PlanDto> PlansList { get; } = new();
        public ObservableCollection<CoursesDto> CoursesList { get; } = new();

        private const int PageSize = 2;
        private int _currentPage = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentPage, value);
                LoadCourses();
            }
        }

        public int TotalPages { get; private set; }

        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PreviousPageCommand { get; }
        public ReactiveCommand<CoursesDto, Unit> BuyCourseCommand { get; }
        public ReactiveCommand<PlanDto, Unit> BuyPlanCommand { get; }

        public ServicePageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            using var ctx = CreateContext();
            var plans = ctx.Plans.ToList();
            foreach (var p in plans)
            {
                PlansList.Add(new PlanDto
                {
                    Plan_id = p.plan_id,
                    Name = p.name,
                    Description = p.description,
                    PriceValue = p.price ?? 0m,
                    Duration = $"{p.duration} месяц"
                });
            }

            var totalCourses = ctx.Courses.Count();
            TotalPages = (int)Math.Ceiling((double)totalCourses / PageSize);

            NextPageCommand = ReactiveCommand.Create(() =>
            {
                if (CurrentPage < TotalPages) CurrentPage++;
            });
            PreviousPageCommand = ReactiveCommand.Create(() =>
            {
                if (CurrentPage > 1) CurrentPage--;
            });

            BuyCourseCommand = ReactiveCommand.CreateFromTask<CoursesDto>(async c =>
                await ProcessPurchaseAsync(c.Course_id, false, c.PriceValue));
            BuyPlanCommand =
                ReactiveCommand.CreateFromTask<PlanDto>(async p =>
                    await ProcessPurchaseAsync(p.Plan_id, true, p.PriceValue));

            LoadCourses();
        }

        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .Options;
            return new AppDbContext(options);
        }

        private void LoadCourses()
        {
            CoursesList.Clear();
            using var ctx = CreateContext();
            var courses = ctx.Courses
                .Include(c => c.Instructor)
                .OrderBy(c => c.course_id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            foreach (var c in courses)
            {
                var author = c.Instructor != null
                    ? $"{c.Instructor.first_name} {c.Instructor.last_name}"
                    : "Неизвестен";

                CoursesList.Add(new CoursesDto
                {
                    Course_id = c.course_id,
                    Title = c.title,
                    Description = c.description,
                    PriceValue = c.price ?? 0m,
                    Author = author
                });
            }
        }

        private async Task ProcessPurchaseAsync(int item_id, bool isPlan, decimal price)
        {
            try
            {
                decimal serviceFee = price * 0.25m;
                decimal tax = price * 0.25m;
                decimal authorShare = price - serviceFee - tax;

                using var ctx = CreateContext();

                var user = Locator.Current.GetService<Users>()
                           ?? throw new InvalidOperationException("User not found");

                var dbUser = await ctx.Users.FindAsync(user.user_id);
                if (dbUser == null || dbUser.balance < price)
                {
                    new NotificationWindow(false).Show();
                    return;
                }
                
                if (isPlan)
                {
                    var subscription = new Subscriptions
                    {
                        user_id = dbUser.user_id,
                        plan_id = item_id,
                        start_date = DateTime.Now,
                        end_date = DateTime.Now.AddMonths(1),
                        status = 1
                    };
                    ctx.Subscriptions.Add(subscription);

                    var payment = new Payments
                    {
                        user_id = dbUser.user_id,
                        course_id = null,
                        plan_id = item_id,
                        amount = price,
                        service_fee = serviceFee,
                        tax = tax,
                        author_share = 0,
                        is_plan = true,
                        created_at = DateTime.UtcNow
                    };
                    ctx.Payments.Add(payment);

                    dbUser.balance -= price;
                }
                else
                {
                    var course = await ctx.Courses
                        .Include(c => c.Instructor)
                        .FirstOrDefaultAsync(c => c.course_id == item_id);

                    if (course == null)
                    {
                        new NotificationWindow(false).Show();
                        return;
                    }

                    var instructorUserId = await ctx.Instructor
                        .Where(i => i.instructor_id == course.instructor_id)
                        .Select(i => i.user_id)
                        .FirstOrDefaultAsync();

                    if (instructorUserId == 0)
                    {
                        new NotificationWindow(false).Show();
                        return;
                    }

                    dbUser.balance -= price;

                    var author = await ctx.Users.FindAsync(instructorUserId);
                    if (author != null)
                    {
                        author.balance += authorShare;
                    }

                    var payment = new Payments
                    {
                        user_id = dbUser.user_id,
                        course_id = course.course_id,
                        plan_id = null,
                        amount = price,
                        service_fee = serviceFee,
                        tax = tax,
                        author_share = authorShare,
                        is_plan = false,
                        created_at = DateTime.UtcNow
                    };
                    ctx.Payments.Add(payment);
                    await ctx.SaveChangesAsync();


                    var user_course = new UserCourse
                    {
                        user_id = dbUser.user_id,
                        pay_id = payment.pay_id, 
                        course_id = course.course_id,
                    };
                    
                    ctx.UserCourses.Add(user_course);
                }

                await ctx.SaveChangesAsync();

                new NotificationWindow(true).Show();
            }
            catch (Exception ex)
            {
                new NotificationWindow(false).Show();
                Console.WriteLine(ex);
            }
        }
    }


    public class PlanDto
    {
        public int Plan_id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceValue { get; set; }
        public string Price => $"{PriceValue:F2}$";
        public string Duration { get; set; } = string.Empty;
    }

    public class CoursesDto
    {
        public int Course_id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceValue { get; set; }
        public string Price => $"{PriceValue:F2}$";
        public string? Author { get; set; }
    }
    
}
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using UnicronPlatform.Data;
using UnicronPlatform.Models;

namespace UnicronPlatform.ViewModels
{
    public class ServicePageViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Магазин";
        public IScreen? HostScreen { get; }

        private Plans _plan;
        private Plans plan
        {
            get => _plan;
            set => this.RaiseAndSetIfChanged(ref _plan, value);
        }

        private ObservableCollection<PlanDto> _plansList;
        public ObservableCollection<PlanDto> PlansList
        {
            get => _plansList;
            set => this.RaiseAndSetIfChanged(ref _plansList, value);
        }

        // private Courses courses; 
        //
        // private ObservableCollection<CoursesDto> _coursesList;
        // public ObservableCollection<CoursesDto> CoursesList
        // {
        //     get => _coursesList;
        //     set => this.RaiseAndSetIfChanged(ref _coursesList, value);
        // }
        public ServicePageViewModel(IScreen hostScreen, Plans plan)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            this.plan = plan;

            PlansList = new ObservableCollection<PlanDto>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;",
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=MyFDB;user=root;password=demo1fort;"))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var plans = context.Plans.ToList();
                foreach (Plans p in plans)
                {
                    PlansList.Add(new PlanDto
                    {
                        Name = p.name,
                        Description = p.description,
                        Price = $"{p.price}$",
                        Duration = $"{p.duration} Месяц"
                    });
                }
            }
            
            // using (var context = new AppDbContext(options))
            // {
            //     var courses = context.Courses.ToList();
            //     var author_name = context.Instructor
            //         .Select(i => new 
            //         {
            //             FirstName = i.first_name,
            //             LastName = i.last_name
            //         })
            //         .ToListAsync();
            //     foreach (Courses c in courses)
            //     {
            //         CoursesList.Add(new CoursesDto
            //         {
            //             Title = c.title,
            //             Description = c.description,
            //             Price = c.price,
            //             Author = author_name.ToString()
            //         });
            //     }
            // }
        }
    }

    public class PlanDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
    }
    
    // public class CoursesDto
    // {
    //     public string Title { get; set; }
    //     public string Description { get; set; }
    //     public decimal? Price { get; set; }
    //     public string Author { get; set; }
    // }
    
}

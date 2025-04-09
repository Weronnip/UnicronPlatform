using Avalonia.ReactiveUI;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views
{
    public partial class HomePage : ReactiveWindow<ShellViewModel>
    {
        public HomePage(AppDbContext dbContext, Users currentUser)
        {
            InitializeComponent();

            var shellVm = new ShellViewModel(dbContext, currentUser);
            ViewModel = shellVm;

            this.WhenActivated(d =>
            {
                // можно логировать или подписывать события здесь
            });
        }
    }
}
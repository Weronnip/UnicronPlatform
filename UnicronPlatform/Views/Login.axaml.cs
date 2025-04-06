using Avalonia.Controls;
using UnicronPlatform.Data;
using UnicronPlatform.ViewModels;

namespace UnicronPlatform.Views;

public partial class Login : Window
{
    public Login(AppDbContext dbContext)
    {
        InitializeComponent();
        DataContext = new LoginUserViewModel(dbContext);
    }
}
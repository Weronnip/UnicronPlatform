using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ReactiveUI;
using UnicronPlatform.Data;
using UnicronPlatform.Models;
using UnicronPlatform.ViewModels;
using UnicronPlatform.Views.Student.Pages;

namespace UnicronPlatform.Views;

public partial class HomePage : Window
{
    private readonly AppDbContext _dbContext;
    private readonly Users _currentUser;
    public RoutingState Router { get; } = new();
    

    public HomePage(AppDbContext dbContext, Users currentUser)
    {
        InitializeComponent();
        
        _dbContext = dbContext;
        _currentUser = currentUser;

        var profileView = new ProfilePage
        {
            DataContext = new ProfilePageViewModel(new ProfilePageViewModel.DummyScreen(), _currentUser, _dbContext)
        };

        this.Content = profileView;
    }
}

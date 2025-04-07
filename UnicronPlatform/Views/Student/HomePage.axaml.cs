using System;
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
    public RoutingState Router { get; } = new RoutingState();

    public HomePage(AppDbContext dbContext, Users currentUser)
    {
        InitializeComponent();
        
        _dbContext = dbContext;
        _currentUser = currentUser;

        // Создаём ViewModel для HomePage
        var homePageViewModel = new HomePageViewModel(_dbContext, currentUser);
        DataContext = homePageViewModel;

        // Стартуем с ProfilePage
        var profileViewModel = new ProfilePageViewModel(homePageViewModel, _currentUser, _dbContext);
        Router.Navigate.Execute(profileViewModel).Subscribe();
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace UnicronPlatform.Views;

public partial class ProfilePage : Window
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private void DragPanel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            BeginMoveDrag(e);
        }
    }
}
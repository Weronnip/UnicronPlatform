using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace UnicronPlatform.Views;

public partial class Sidebar : ReactiveUserControl<Sidebar>
{
    public Sidebar()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
        });
    }
}
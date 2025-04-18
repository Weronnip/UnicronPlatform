using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnicronPlatform.Views
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(bool isSuccess, int autoCloseMs = 3000)
        {
            InitializeComponent();
            DataContext = new NotificationWindowViewModel(isSuccess);
            AutoCloseAfterDelay(autoCloseMs);
        }

        private async void AutoCloseAfterDelay(int delay)
        {
            await Task.Delay(delay);
            Close();
        }
    }
}
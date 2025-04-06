using System.Reactive.Disposables;
using UnicronPlatform.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;

namespace UnicronPlatform.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                // Регистрируем обработчик диалогов
                ViewModel!.ShowDialog.RegisterHandler(DoShowDialog)
                    .DisposeWith(disposables);
            });
        }

        private async Task DoShowDialog(IInteractionContext<ViewModelBase, ViewModelBase?> interaction)
        {
            if (interaction.Input is RegistrationUserViewModel)
            {
                var registrationWindow = new Registration()
                {
                    DataContext = interaction.Input
                };
                var result = await registrationWindow.ShowDialog<ViewModelBase>(this);
                interaction.SetOutput(result);
            }
            else if (interaction.Input is LoginUserViewModel)
            {
                if (this.DataContext is MainWindowViewModel mainVm)
                {
                    var loginWindow = new Login(mainVm._dbContext)
                    {
                        DataContext = interaction.Input
                    };
                    var result = await loginWindow.ShowDialog<ViewModelBase>(this);
                    interaction.SetOutput(result);
                }
                else
                {
                    interaction.SetOutput(null);
                }
            }
            else
            {
                interaction.SetOutput(null);
            }
        }
    }
}
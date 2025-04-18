using ReactiveUI;

public class NotificationWindowViewModel : ReactiveObject
{
    private bool _isSuccess;
    public bool IsSuccess
    {
        get => _isSuccess;
        set => this.RaiseAndSetIfChanged(ref _isSuccess, value);
    }

    public bool IsError => !IsSuccess;

    public NotificationWindowViewModel(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}
using NYC311Dashboard.Components;

namespace NYC311Dashboard.Services.Contracts
{
    public interface IMessagingService
    {
        ConfirmDialog confirmDialog { get; set; }

        event Action? OnMessageChanged;
        MessageType CurrentType { get; set; }
        string? Message { get; set; }

        void ShowInfo(string? message = null);
        void ShowError(string? message = null);

        void Clear();

        void ShowErrorDialog(string message);
        void ShowDialog(string message, Func<Task> onConfirm);
        void OnDialogClosed(bool confirmed);
    }
}

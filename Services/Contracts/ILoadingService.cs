namespace NYC311Dashboard.Services.Contracts
{
    public interface ILoadingService
    {
        event Action? OnLoadingChanged;

        bool IsLoading { get; set; }

        string LoadingMessage { get; set; }
    }
}

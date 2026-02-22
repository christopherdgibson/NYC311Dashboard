using CSharpFunctionalExtensions;

namespace NYC311Dashboard.Intrastructure.Contracts
{
    public interface IHttpService
    {
        Task<Result<T>> GetAsync<T>(string url) where T : class;
    }
}

using Microsoft.JSInterop;
using NYC311Dashboard.Intrastructure.Contracts;
using NYC311Dashboard.Services.Contracts;
using NYC311Dashboard.Services.Models;

namespace NYC311Dashboard.Services
{
    public class PdfService : IPdfService
    {
        private readonly IJSRuntime _js;
        private readonly IHttpService _httpService;
        private readonly ILoadingService _loadingService;
        private readonly IMessagingService _messagingService;

        public PdfService(IJSRuntime js, ILoadingService loadingService, IMessagingService messagingService)
        {
            _js = js;
            _loadingService = loadingService;
            _messagingService = messagingService;
        }

        public async Task RenderPdfDownload(string elementId, PdfOptions options)
        {
            try
            {
                _loadingService.LoadingMessage = "I'm loading here!";
                _loadingService.IsLoading = true;

                await _js.InvokeVoidAsync("saveElementAsPdf", "pdf-content", options);
            }
            catch
            {
                _messagingService.ShowError("An error occurred. Please try again.!");
                //return Result.Failure("An error occurred. Please try again.!");
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }
    }
}

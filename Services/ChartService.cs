using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using NYC311Dashboard.Services.Contracts;
using NYC311Dashboard.Services.Models;

namespace NYC311Dashboard.Services
{
    public class ChartService : IChartService
    {
        private readonly IJSRuntime _js;
        private readonly ILoadingService _loadingService;
        private readonly IMessagingService _messagingService;
        public ChartOptions BarChartByBorough { get; private set; }
        public ChartOptions LineChartByZipHour { get; private set; }

        public ChartService(IJSRuntime js, ILoadingService loadingService, IMessagingService messagingService)
        {
            _js = js;
            _loadingService = loadingService;
            _messagingService = messagingService;
        }

        public Result<ChartOptions> GetBarChartOptions(string selection, List<string> categories, List<ApexSeries> series, string? width = null, string? height = null)
        {
            try
            {
                _loadingService.LoadingMessage = "I'm loading here!";
                _loadingService.IsLoading = true;
                string type;
                if (selection == "boroughs")
                {
                    type = "bar";
                }
                else
                {
                    type = "line";
                }

                if (!categories.Any())
                {
                    _messagingService.ShowInfo($"No {selection} selected!");
                    _loadingService.IsLoading = false;
                    return Result.Failure<ChartOptions>("No boroughs selected!");
                }

                var options = new ChartOptions
                {
                    Chart = new Chart { Type = type },
                    XAxis = new XAxis { Categories = categories },
                    Series = series,
                    Width = width,
                    Height = height
                };

                if (type == "bar")
                {
                    BarChartByBorough = options;
                }
                if (type == "line")
                {
                    LineChartByZipHour = options;
                }
                // set to no error here?

                _messagingService.Clear();
                return Result.Success(options);
            }
            catch
            {
                _messagingService.ShowError("Failed to get chart options.");
                return Result.Failure<ChartOptions>("Failed to get chart options.");
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }

        public async Task RenderBarChart(ChartOptions options)
        {
            try
            {
                _loadingService.LoadingMessage = "I'm loading here!";
                _loadingService.IsLoading = true;

                await _js.InvokeVoidAsync("renderApexBarChart", options);
                BarChartByBorough = options;
            }
            catch
            {
                _messagingService.ShowError("Failed to render chart. Please check your connection and try again."); //todo: dialog box with error message and retry button?
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }

        public async Task RenderLineChart(ChartOptions options)
        {
            try
            {
                _loadingService.LoadingMessage = "I'm loading here!";
                _loadingService.IsLoading = true;

                await _js.InvokeVoidAsync("renderApexChartMulti", options);
                LineChartByZipHour = options;
            }
            catch
            {
                _messagingService.ShowError("Failed to render chart. Please check your connection and try again.");
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }

        public async Task UpdateApexChart(ChartOptions options)
        {
            await _js.InvokeVoidAsync("updateApexChart", options);
        }

        public async Task DisposeApexChart()
        {
            await _js.InvokeVoidAsync("disposeApexChart");
        }
    }
}

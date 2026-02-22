using CSharpFunctionalExtensions;
using NYC311Dashboard.Services.Models;

namespace NYC311Dashboard.Services.Contracts
{
    public interface IChartService
    {
        ChartOptions BarChartByBorough { get; }
        ChartOptions LineChartByZipHour { get; }

        Result<ChartOptions> GetBarChartOptions(string selection, List<string> categories, List<ApexSeries> series, string? width = null, string? height = null);

        Task RenderBarChart(ChartOptions options);

        Task RenderLineChart(ChartOptions options);

        Task UpdateApexChart(ChartOptions options);

        Task DisposeApexChart();
    }
}

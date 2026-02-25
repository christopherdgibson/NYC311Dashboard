using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NYC311Dashboard.Intrastructure;
using NYC311Dashboard.Intrastructure.Contracts;
using NYC311Dashboard.Services;
using NYC311Dashboard.Services.Contracts;
using System.Text.Json;

namespace NYC311Dashboard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ISidebarService, SidebarService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IChartService, ChartService>();
            builder.Services.AddScoped<IMessagingService, MessagingService>();
            builder.Services.AddScoped<ILoadingService, LoadingService>();
            builder.Services.AddScoped<IPdfService, PdfService>();
            builder.Services.Configure<JsonSerializerOptions>(options =>
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            });
            await builder.Build().RunAsync();
        }
    }
}

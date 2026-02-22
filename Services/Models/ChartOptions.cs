using System.Text.Json.Serialization;

namespace NYC311Dashboard.Services.Models
{
    public class ChartOptions
    {
        public Chart Chart { get; set; } = new Chart();

        [JsonPropertyName("xaxis")]
        public XAxis XAxis { get; set; } = new XAxis();
        public List<ApexSeries> Series { get; set; } = new();
        public object Width { get; set; } = "100%";
        public object Height { get; set; } = "100%";

        public bool HasData =>
            (XAxis?.Categories?.Count ?? 0) > 0
            && (Series?.Any(s => s.Data?.Count > 0) ?? false);
    }

    public class ApexSeries
    {
        public string Name { get; set; }
        public List<double> Data { get; set; }
    }

    public class Chart
    {
        public string Type { get; set; } = "bar";
    }

    public class XAxis
    {
        public List<string> Categories { get; set; } = new();
    }
}

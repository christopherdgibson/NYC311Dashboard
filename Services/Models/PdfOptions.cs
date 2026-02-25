using System.Text.Json.Serialization;

namespace NYC311Dashboard.Services.Models
{
    public class PdfOptions
    {
        [JsonPropertyName("margin")]
        public int[] Margin { get; set; } = [10, 10, 10, 10];

        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = "fileName.pdf";

        [JsonPropertyName("image")]
        public ImageOptions Image { get; set; } = new ImageOptions { Type = "jpeg", Quality = 0.98 };

        [JsonPropertyName("html2canvas")]
        public Html2CanvasOptions Html2Canvas { get; set; } = new Html2CanvasOptions { Scale = 2, WindowWidth = 794 }; // A4 at 96dpi

        [JsonPropertyName("jsPDF")]
        public JsPdfOptions JsPDF { get; set; } = new JsPdfOptions { Unit = "mm", Format = "a4", Orientation = "portrait" };

        [JsonPropertyName("pagebreak")]
        public PageBreak PageBreak { get; set; } = new PageBreak { Mode = ["css", "avoid-all"] };
    }

    public class ImageOptions
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("quality")]
        public double Quality { get; set; }
    }

    public class Html2CanvasOptions
    {
        [JsonPropertyName("scale")]
        public int Scale { get; set; }
        [JsonPropertyName("windowWidth")]
        public int WindowWidth { get; set; }
    }

    public class JsPdfOptions
    {
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("orientation")]
        public string Orientation { get; set; }
    }

    public class PageBreak
    {
        [JsonPropertyName("mode")]
        public string[] Mode { get; set; }

        [JsonPropertyName("avoid")]
        public string[] Avoid { get; set; }
    }
}

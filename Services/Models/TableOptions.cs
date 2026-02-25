namespace NYC311Dashboard.Services.Models
{
    public class TableOptions<TItem>
    {
        public Func<TItem, string>? GroupBy { get; set; }
        public string? TableStyles { get; set; }
    }
}

namespace NYC311Dashboard.Models.Contracts
{
    public interface IRequestModelBase
    {
        string Borough { get; set; }
        DateTime? CreatedDate { get; set; }
        double OpenTime { get; set; }
    }
}

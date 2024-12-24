namespace OT.Assessment.Core.Queries.LoadPlayerWagers
{
    public class LoadPlayerWagersResponse
    {
        public Guid WagerId { get; set; }
        public string Game { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

namespace OT.Assessment.Core.Queries.LoadTopSpenders
{
    public class LoadTopSpendersResponse
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public decimal TotalAmountSpend { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Infrastructure.Entities
{
    public class Wager
    {
        [Key]
        public Guid WagerId { get; set; }
        public string Theme { get; set; }
        public string Provider { get; set; }
        public string GameName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid PlayerAccountId { get; set; }
        public virtual Player Player { get; set; }
    }
}

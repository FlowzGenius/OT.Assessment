using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Infrastructure.Entities
{
    public class Player
    {
        [Key]
        public Guid AccountId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<Wager> Wagers { get; } = [];
    }
}

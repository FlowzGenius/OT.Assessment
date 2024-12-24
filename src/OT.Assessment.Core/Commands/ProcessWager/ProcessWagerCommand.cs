using MediatR;

namespace OT.Assessment.Core.Commands.ProcessWager
{
    public class ProcessWagerCommand : IRequest
    {
        public Guid WagerId { get; set; }
        public string Theme { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;   
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}

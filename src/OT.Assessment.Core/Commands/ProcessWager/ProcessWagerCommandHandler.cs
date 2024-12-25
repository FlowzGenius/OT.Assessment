using MediatR;
using Microsoft.EntityFrameworkCore;
using OT.Assessment.Infrastructure.Context;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Core.Commands.ProcessWager;
public sealed class ProcessWagerCommandHandler(ApplicationDbContext applicationDbContext) 
    : IRequestHandler<ProcessWagerCommand>
{
    public async Task Handle(ProcessWagerCommand request, CancellationToken cancellationToken)
    {
        if(!await PlayerExistsAsync(request.AccountId))
        {
            var player = new Player()
            {
                AccountId = request.AccountId,
                UserName = request.Username,
            };

            await applicationDbContext.Player.AddAsync(player, cancellationToken);
        }

        var wager = new Wager()
        {
            WagerId = request.WagerId,
            Theme = request.Theme,
            Provider = request.Provider,
            GameName = request.GameName,
            Amount = request.Amount,
            CreationDate = request.CreationDate,
            PlayerAccountId = request.AccountId
        };

        await applicationDbContext.Wager.AddAsync(wager, cancellationToken);

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> PlayerExistsAsync(Guid playerId)
    {
        return await applicationDbContext.Player.AnyAsync(x => x.AccountId == playerId);
    }
}


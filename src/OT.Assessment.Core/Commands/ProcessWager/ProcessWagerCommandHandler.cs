using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assessment.Infrastructure.Context;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Core.Commands.ProcessWager;
public sealed class ProcessWagerCommandHandler(ApplicationDbContext applicationDbContext, ILogger<ProcessWagerCommandHandler> logger) 
    : IRequestHandler<ProcessWagerCommand>
{
    public async Task Handle(ProcessWagerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!await PlayerExistsAsync(request.AccountId))
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process wager for account with id: {accountId}", request.AccountId);
            throw;
        }
    }

    public async Task<bool> PlayerExistsAsync(Guid playerId)
        => await applicationDbContext.Player.AnyAsync(x => x.AccountId == playerId);
    
}


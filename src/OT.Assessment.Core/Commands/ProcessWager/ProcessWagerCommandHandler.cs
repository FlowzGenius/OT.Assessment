﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OT.Assessment.Infrastructure.Context;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Core.Commands.ProcessWager;
public sealed class ProcessWagerCommandHandler(ApplicationDbContext applicationDbContext, TimeProvider timeProvider) 
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

            await applicationDbContext.Player.AddAsync(player);
        }

        var wager = new Wager()
        {
            WagerId = request.WagerId,
            Theme = request.Theme,
            Provider = request.Provider,
            GameName = request.GameName,
            Amount = request.Amount,
            CreationDate = timeProvider.GetUtcNow().DateTime,
            PlayerAccountId = request.AccountId
        };

        await applicationDbContext.Wager.AddAsync(wager);

        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<bool> PlayerExistsAsync(Guid playerId)
    {
        return await applicationDbContext.Player.AnyAsync(x => x.AccountId == playerId);
    }
}


﻿using MassTransit;
using MediatR;
using OT.Assessment.Core.Commands.ProcessWager;
using OT.Assessment.Messaging.Models;

namespace OT.Assessment.Consumer
{
    public class WagerEventConsumer(IMediator mediator) : IConsumer<CasinoWagerEvent>
    {
        public async Task Consume(ConsumeContext<CasinoWagerEvent> context)
        {
            var casinoWager = context.Message;
            var processWagerCommand = new ProcessWagerCommand()
            {
                WagerId = Guid.Parse(casinoWager.WagerId),
                Theme = casinoWager.Theme,
                Provider = casinoWager.Provider,
                GameName = casinoWager.GameName,
                Amount = (decimal)casinoWager.Amount,
                AccountId = Guid.Parse(casinoWager.AccountId),
                Username = casinoWager.Username
            };

           await mediator.Send(processWagerCommand);
        }
    }
}

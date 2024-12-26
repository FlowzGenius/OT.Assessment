using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Core.Queries.LoadTopSpenders
{
    public class LoadTopSpendersQueryHandler(ApplicationDbContext dbContext, ILogger<LoadTopSpendersQueryHandler> logger) 
        : IRequestHandler<LoadTopSpendersQuery, IReadOnlyList<LoadTopSpendersResponse>>
    {
        public async Task<IReadOnlyList<LoadTopSpendersResponse>> Handle(LoadTopSpendersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var players = await dbContext.Player
                    .AsNoTracking()
                    .Include(x => x.Wagers)
                    .Select(x => new LoadTopSpendersResponse()
                    {
                        TotalAmountSpend = x.Wagers.Sum(y => y.Amount),
                        AccountId = x.AccountId,
                        Username = x.UserName
                    }).ToListAsync();

                var topSpenders = players
                    .OrderByDescending(x => x.TotalAmountSpend)
                    .Take(request.Count)
                    .ToList()
                    .AsReadOnly<LoadTopSpendersResponse>();

                return topSpenders;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load top spenders");
                throw;
            }
        }
    }
}

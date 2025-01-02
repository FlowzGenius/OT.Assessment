using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Core.Queries.LoadPlayerWagers
{
    public sealed class LoadPlayerWagersQueryHandler(ApplicationDbContext dbContext, ILogger<LoadPlayerWagersQueryHandler> logger) 
        : IRequestHandler<LoadPlayerWagersQuery, PagedResult<LoadPlayerWagersResponse>>
    {
        public async Task<PagedResult<LoadPlayerWagersResponse>> Handle(LoadPlayerWagersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var players = dbContext.Wager.AsNoTracking().Where(x => x.PlayerAccountId == request.PlayerId);
                var total = await players.CountAsync(cancellationToken);
                var totalPages = (int)Math.Ceiling((double)total / request.PageSize);
                var skip = (request.Page - 1) * request.PageSize;

                var result = players.Where(x => x.PlayerAccountId == request.PlayerId)
                    .Skip(skip)
                    .Take(request.PageSize)
                    .Select(x => new LoadPlayerWagersResponse
                    {
                        WagerId = x.WagerId,
                        Amount = x.Amount,
                        Game = x.GameName,
                        Provider = x.Provider,
                        CreatedDate = x.CreationDate
                    });
                return new PagedResult<LoadPlayerWagersResponse>
                {
                    Data = result,
                    TotalPages = totalPages,
                    Page = request.Page,
                    Total = total,
                    PageSize = request.PageSize
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch wagers for player with id: {accountId}", request.PlayerId);
                throw;
            }
        }
    }
}

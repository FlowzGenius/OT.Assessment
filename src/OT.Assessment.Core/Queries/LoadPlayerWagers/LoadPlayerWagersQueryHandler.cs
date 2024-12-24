using MediatR;
using Microsoft.EntityFrameworkCore;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Core.Queries.LoadPlayerWagers
{
    public sealed class LoadPlayerWagersQueryHandler(ApplicationDbContext dbContext) 
        : IRequestHandler<LoadPlayerWagersQuery, PagedResult<LoadPlayerWagersResponse>>
    {
        public async Task<PagedResult<LoadPlayerWagersResponse>> Handle(LoadPlayerWagersQuery request, CancellationToken cancellationToken)
        {
            var total = await dbContext.Wager.CountAsync(cancellationToken);
            var totalPages = total / request.PageSize;
            var skip = (request.Page - 1) * request.PageSize;

            var result = dbContext.Wager.Where(x => x.PlayerAccountId == request.PlayerId)
                .AsNoTracking()
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
    }
}

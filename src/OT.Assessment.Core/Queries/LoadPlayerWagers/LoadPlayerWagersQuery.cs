using MediatR;
namespace OT.Assessment.Core.Queries.LoadPlayerWagers
{
    public class LoadPlayerWagersQuery : IRequest<PagedResult<LoadPlayerWagersResponse>>
    {
        public required Guid PlayerId { get; set; }
        public required int PageSize { get; set; }
        public required int Page { get; set; }
    }
}

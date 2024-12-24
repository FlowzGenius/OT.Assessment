using MediatR;
namespace OT.Assessment.Core.Queries.LoadPlayerWagers
{
    public class LoadPlayerWagersQuery : IRequest<PagedResult<LoadPlayerWagersResponse>>
    {
        public Guid PlayerId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}

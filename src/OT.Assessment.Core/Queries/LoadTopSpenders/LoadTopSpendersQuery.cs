using MediatR;

namespace OT.Assessment.Core.Queries.LoadTopSpenders
{
    public class LoadTopSpendersQuery : IRequest<IReadOnlyList<LoadTopSpendersResponse>>
    {
        public int Count { get; set; }
    }
}

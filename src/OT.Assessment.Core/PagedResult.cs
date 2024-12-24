namespace OT.Assessment.Core
{
    public class PagedResult<T>
    {
        public IQueryable<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}

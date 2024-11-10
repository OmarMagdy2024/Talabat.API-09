namespace Talabat.API.ModelDTO
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Date { get; set; }

        public Pagination(int pageSize, int pageindex,int total, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageindex;
            TotalCount= total;
            Date = data;
        }
    }
}

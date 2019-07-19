
namespace Framework.Core.Pagination
{
    public class PageQuery
    {
        public PageQuery()
        {
            PageSize = 10;
            PageIndex = 1;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
}

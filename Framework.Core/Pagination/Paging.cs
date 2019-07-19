using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Pagination
{
    public class Paging<T> : IPaging<T>
    {
        public IEnumerable<T> Content { get; set; }
        public int TotalRecord { get; set; }
        public object AdditionalData { get; set; }
    }

    public static class Paging
    {
        public static IQueryable<T> ToPagingAndSorting<T>(this IQueryable<T> dataModels, PageQuery objectQuery)
        {
            return objectQuery.PageSize == 0
                ? dataModels
                : dataModels
                    .Skip((objectQuery.PageIndex - 1) * objectQuery.PageSize)
                    .Take(objectQuery.PageSize);
        }

        public static IEnumerable<T> ToPagingAndSorting<T>(this IEnumerable<T> dataModels, PageQuery objectQuery)
        {
            return objectQuery.PageSize == 0
                ? dataModels
                : dataModels
                    .Skip((objectQuery.PageIndex - 1) * objectQuery.PageSize)
                    .Take(objectQuery.PageSize);
        }

        public static Paging<T> ToPaging<T>(this IEnumerable<T> data, int totalRecord)
        {
            return new Paging<T>
            {
                Content = data,
                TotalRecord = totalRecord
            };
        }

        public static Paging<T> ToPaging<T>(this IEnumerable<T> data, object additionalData, int totalRecord)
        {
            return new Paging<T>
            {
                Content = data,
                TotalRecord = totalRecord,
                AdditionalData = additionalData
            };
        }
    }
}

using System.Collections.Generic;

namespace Framework.Core.Pagination
{
    public interface IPaging<T>
    {
        IEnumerable<T> Content { get; set; }

        int TotalRecord { get; set; }
    }
}
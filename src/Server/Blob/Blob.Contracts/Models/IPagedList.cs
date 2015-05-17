
using System.Collections.Generic;

namespace Blob.Contracts.Models
{
    public interface IPagedList<T>
    {
        bool HasNext { get; set; }
        bool HasPrevious { get; set; }
        int PageNum { get; set; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        IEnumerable<T> Items { get; set; } 
    }
}

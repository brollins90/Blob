
using System.Collections.Generic;

namespace Blob.Contracts.Models
{
    public interface IPageInfoVm<T>
    {
        int PageNum { get; set; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        IEnumerable<T> Items { get; set; } 
    }
}

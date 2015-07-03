namespace Blob.Contracts.ViewModel
{
    using System.Collections.Generic;

    public interface IPageInfoViewModel<T>
    {
        int PageNum { get; set; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}
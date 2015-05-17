//using System.Collections.Generic;
//using System.Linq;

//namespace Blob.Contracts.Models
//{
//    // http://www.talksharp.com/entity-framework-paging
//    public class PagedList<T> : List<T>, IPagedList
//    {
//        public bool HasNext { get; set; }
//        public bool HasPrevious { get; set; }
//        public int PageNum { get; set; }
//        public int PageCount { get; set; }
//        public int PageSize { get; set; }
//        public int TotalCount { get; set; }

//        public PagedList(IEnumerable<T> source, int totalCount, int pageNum, int pageSize)
//        {
//            TotalCount = totalCount;
//            PageCount = GetPagedCount(pageSize, TotalCount);
//            PageNum = pageNum < 1 ? 0 : pageNum - 1;
//            PageSize = pageSize;

//            AddRange(source);
//        }

//        private int GetPagedCount(int pageSize, int totalCount)
//        {
//            if (pageSize == 0)
//            {
//                return 0;
//            }

//            int remainder = totalCount % pageSize;
//            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
//        }
//    }
//}

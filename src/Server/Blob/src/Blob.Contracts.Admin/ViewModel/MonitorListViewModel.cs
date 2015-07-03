namespace Blob.Contracts.ViewModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class MonitorListViewModel : IPageInfoViewModel<MonitorListListItem>
    {
        [DataMember]
        public int PageNum { get; set; }

        [DataMember]
        public int PageCount { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public IEnumerable<MonitorListListItem> Items { get; set; }
    }
}
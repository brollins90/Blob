namespace Blob.Contracts.ViewModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class DashDevicesLargeViewModel : IPageInfoViewModel<DashDevicesListItem>
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
        public IEnumerable<DashDevicesListItem> Items { get; set; }
    }
}
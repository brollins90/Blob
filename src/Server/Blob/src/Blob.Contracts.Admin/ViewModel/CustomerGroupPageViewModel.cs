namespace Blob.Contracts.ViewModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerGroupPageViewModel : IPageInfoViewModel<CustomerGroupListItem>
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
        public IEnumerable<CustomerGroupListItem> Items { get; set; }
    }
}
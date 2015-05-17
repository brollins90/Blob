using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class DashDevicesLargeVm : IPagedList<DashDevicesLargeListItemVm>
    {

        [DataMember]
        public bool HasNext { get; set; }

        [DataMember]
        public bool HasPrevious { get; set; }

        [DataMember]
        public int PageNum { get; set; }

        [DataMember]
        public int PageCount { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public IEnumerable<DashDevicesLargeListItemVm> Items {get; set; } 
    }
}

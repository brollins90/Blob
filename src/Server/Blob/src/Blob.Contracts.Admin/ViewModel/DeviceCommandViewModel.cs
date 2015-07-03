namespace Blob.Contracts.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class DeviceCommandViewModel
    {
        [DataMember]
        [Display(Name = "Command type")]
        [Required]
        public string CommandType { get; set; }

        [DataMember]
        [Display(Name = "Short name")]
        [Required]
        public string ShortName { get; set; }

        [DataMember]
        [Display(Name = "Command parameters")]
        public IEnumerable<DeviceCommandParameterPair> CommandParamters { get; set; }
    }
}
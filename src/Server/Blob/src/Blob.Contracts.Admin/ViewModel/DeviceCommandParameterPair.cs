namespace Blob.Contracts.ViewModel
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DeviceCommandParameterPair
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
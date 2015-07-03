namespace Blob.Contracts.Response
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class BlobResult
    {

        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public ICollection<string> Errors { get; set; } = new List<string>();


        public BlobResult()
        {
            Succeeded = true;
        }

        public BlobResult(bool success)
        {
            Succeeded = success;
        }

        public BlobResult(string error)
        {
            Succeeded = false;
            Errors.Add(error);
        }

        public BlobResult(IEnumerable<string> errors)
        {
            Succeeded = false;
            Errors = new List<string>(errors);
        }

        public static BlobResult Success
        {
            get { return new BlobResult(true); }
        }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class BlobResultDto
    {
        public BlobResultDto()
        {
            Succeeded = true;
        }

        public BlobResultDto(bool success)
        {
            Succeeded = success;
        }

        public BlobResultDto(string error)
        {
            Succeeded = false;
            Errors = new List<string>(new[] { error });
        }

        public BlobResultDto(IEnumerable<string> errors)
        {
            Succeeded = false;
            Errors = new List<string>(errors);
        }

        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public IEnumerable<string> Errors
        {
            get { return _errors ?? (_errors = new List<string>()); }
            set { _errors = value; }
        }
        private IEnumerable<string> _errors;
    }
}

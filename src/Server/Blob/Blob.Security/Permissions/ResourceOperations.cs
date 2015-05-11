
using System;
using System.Security;
using log4net;

namespace Blob.Security.Permissions
{
    internal class ResourceOperations
    {
        private ILog _log;
        public ResourceOperations()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing ResourceOperations");
        }

        public bool Authenticated { get; set; }
        public string Resource { get; set; }
        public string Operation { get; set; }


        internal SecurityElement ToXml()
        {
            SecurityElement securityElement = new SecurityElement("ResourceOperation");
            if (this.Authenticated)
                securityElement.AddAttribute("Authenticated", "true");
            if (this.Resource != null)
                securityElement.AddAttribute("Resource", SecurityElement.Escape(this.Resource));
            if (this.Operation != null)
                securityElement.AddAttribute("Operation", SecurityElement.Escape(this.Operation));
            return securityElement;
        }

        internal void FromXml(SecurityElement e)
        {
            string strA = e.Attribute("Authenticated");
            this.Authenticated = strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0;
            string str1 = e.Attribute("Resource");
            this.Resource = str1 == null ? (string)null : str1;
            string str2 = e.Attribute("Operation");
            this.Operation = str2 == null ? (string)null : str2;
        }

        protected bool Equals(ResourceOperations other)
        {
            return string.Equals(Operation, other.Operation) && string.Equals(Resource, other.Resource) && Authenticated.Equals(other.Authenticated);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((ResourceOperations)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Operation != null ? Operation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Resource != null ? Resource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Authenticated.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ResourceOperations left, ResourceOperations right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ResourceOperations left, ResourceOperations right)
        {
            return !Equals(left, right);
        }
    }
}

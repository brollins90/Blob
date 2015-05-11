using System;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using log4net;

namespace Blob.Security.Permissions
{
    [Serializable]
    public class BlobPermission : IPermission, IUnrestrictedPermission
    {
        private ILog _log;
        private readonly bool _specifiedAsUnrestricted = false;
        //private readonly CustomerResourceAction[] Actions;
        private ResourceOperations[] _rows;

        public BlobPermission(PermissionState state)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPermission");

            _specifiedAsUnrestricted = (state == PermissionState.Unrestricted);
            //if (state == PermissionState.Unrestricted)
            //{
            //    _rows = new ResourceOperations[1];
            //    _rows[0] = new ResourceOperations { Authenticated = true, Resource = null, Operation = null };
            //}
            //else if (state == PermissionState.None)
            //{
            //    _rows = new ResourceOperations[1];
            //    _rows[0] = new ResourceOperations { Authenticated = false, Resource = null, Operation = null };
            //}
            //else
            //{
            //    throw new ArgumentException("Invalid permission state");
            //}
        }

        public BlobPermission(string resource, string action, bool authenticated = false)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPermission");

            _specifiedAsUnrestricted = false;
            _rows = new ResourceOperations[1];
            _rows[0] = new ResourceOperations { Authenticated = authenticated, Resource = resource, Operation = action };
        }

        private BlobPermission(ResourceOperations[] array)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPermission");

            _specifiedAsUnrestricted = false;
            _rows = array;//.Any() ? array : new []{ new ResourceAction{Authenticated = false, Action = null,Resource = null}};
        }

        // For debugging, return the state of this object as XML. 
        public override String ToString() { return ToXml().ToString(); }

        // Private method to cast (if possible) an IPermission to the type. 
        private BlobPermission VerifyTypeMatch(IPermission target)
        {
            if (GetType() != target.GetType())
                throw new ArgumentException(String.Format("target must be of the {0} type",
                    GetType().FullName));
            return (BlobPermission)target;
        }

        #region ISecurityEncodable Members
        public void FromXml(SecurityElement elem)
        {
            //CodeAccessPermission.ValidateElement(elem, (IPermission)this);
            if (elem.Children != null && elem.Children.Count != 0)
            {
                int count = elem.Children.Count;
                int num = 0;
                this._rows = new ResourceOperations[count];
                foreach (SecurityElement e in elem.Children)
                {
                    ResourceOperations one = new ResourceOperations();
                    one.FromXml(e);
                    this._rows[num++] = one;
                }
            }
            else
                this._rows = new ResourceOperations[0];
        }
        
        public SecurityElement ToXml()
        {
            SecurityElement e = new SecurityElement("IPermission");
            e.AddAttribute("class", GetType().AssemblyQualifiedName.Replace('\"', '\''));
            e.AddAttribute("version", "1");

            int length = _rows.Length;
            for (int index = 0; index < length; ++index)
                e.AddChild(_rows[index].ToXml());
            return e;
        }
        #endregion
        
        #region IPermission Members
        // Return a new object that matches 'this' object's permissions. 
        public IPermission Copy()
        {
            return new BlobPermission(_rows);
        }

        // Return a new object that contains the intersection of 'this' and 'target'. 
        public IPermission Intersect(IPermission target)
        {
            if (target == null) return null;

            BlobPermission other = VerifyTypeMatch(target);

            if (IsUnrestricted())
                return other.Copy();

            if (other.IsUnrestricted())
                return Copy();

            var intersect = _rows.Intersect(other._rows);
            var ros = intersect as ResourceOperations[] ?? intersect.ToArray();
            return ros.Any() ? new BlobPermission(ros.ToArray()) : null;
        }

        // Called by the Demand method: returns true if 'this' is a subset of 'target'. 
        public bool IsSubsetOf(IPermission target)
        {
            if (target == null || (target as BlobPermission) == null)
                return IsEmpty();
            try
            {
                var other = (BlobPermission)target;
                if (other.IsUnrestricted())
                    return true;

                if (this.IsUnrestricted())
                    return false;

                for (int iThis = 0; iThis < _rows.Length; ++iThis)
                {
                    bool flag = false;
                    for (int iOther = 0; iOther < other._rows.Length; ++iOther)
                    {
                        if (other._rows[iOther].Equals(_rows[iThis]))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                        return false;
                }
                return true;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Target is not a BlobPermission");
            }
        }

        // Return a new object that contains the union of 'this' and 'target'. 
        public IPermission Union(IPermission target)
        {
            if (!(target is BlobPermission))
                return null;

            var other = (BlobPermission)target;
            if (IsUnrestricted())
                return other.Copy();

            if (other.IsUnrestricted())
                return Copy();

            var list = _rows.Union(other._rows);
            var ros = list as ResourceOperations[] ?? list.ToArray();
            return ros.Any() ? new BlobPermission(ros.ToArray()) : null;
        }
        #endregion


        #region IUnrestrictedPermission Members
        // Returns true if permission is effectively unrestricted.
        public bool IsUnrestricted()
        {
            //return _specifiedAsUnrestricted;
            return _rows.All(cra => cra.Authenticated && !string.IsNullOrEmpty(cra.Operation) && !string.IsNullOrEmpty(cra.Resource));
        }
        #endregion

        public void Demand()
        {
            new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
            var principal = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (principal == null)
                ThrowSecurityException();

            if (_rows == null)
                return;

            bool pass = false;
            foreach (var item in _rows)
            {
                if (item.Authenticated)
                {
                    if (principal.Identity.HasClaimFor(resource: item.Resource, operation: item.Operation))
                    {
                        pass = true;
                        break;
                    }
                }
                else
                {
                    pass = true;
                    break;
                }
            }
            if (pass)
                return;
            ThrowSecurityException();
        }

        private void ThrowSecurityException()
        {
            //AssemblyName assemblyName = (AssemblyName)null;
            //Evidence evidence = (Evidence)null;
            ////PermissionSet.s_fullTrust.Assert();
            //try
            //{
            //    Assembly callingAssembly = Assembly.GetCallingAssembly();
            //    assemblyName = callingAssembly.GetName();
            //    if (callingAssembly != Assembly.GetExecutingAssembly())
            //        evidence = callingAssembly.Evidence;
            //}
            //catch
            //{
            //}
            //PermissionSet.RevertAssert();
            //throw new SecurityException("Blob permission error", assemblyName, (PermissionSet)null, (PermissionSet)null, (MethodInfo)null, SecurityAction.Demand, (object)this, (IPermission)this, evidence);
            throw new SecurityException();
        }

        private bool IsEmpty()
        {
            return _rows.All(cra => !cra.Authenticated && !string.IsNullOrEmpty(cra.Operation) && !string.IsNullOrEmpty(cra.Resource));
        }
        public override bool Equals(object obj)
        {
            IPermission target = obj as IPermission;
            return (obj == null || target != null) && IsSubsetOf(target) && (target == null || target.IsSubsetOf(this));
        }
        public override int GetHashCode()
        {
            return _rows.Sum(t => t.GetHashCode());
        }

    }
}

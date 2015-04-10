using Blob.Contracts.Models;
using System.Threading.Tasks;

namespace Blob.Managers.Registration
{
    public interface IRegistrationManager
    {
        Task<RegistrationInformation> RegisterDevice(RegistrationMessage message);
    }
}

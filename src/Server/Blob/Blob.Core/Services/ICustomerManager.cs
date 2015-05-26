using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Core.Services
{
    public interface ICustomerManager
    {
        void RegisterCustomer(RegisterCustomerDto dto);
    }
}

﻿using System;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity
{
    public interface ICustomerStore
    {
        Task CreateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<Customer> FindByIdAsync(Guid customerId);
        Task<Customer> FindByNameAsync(string customerName);
        Task UpdateAsync(Customer customer);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;

namespace DataLayer
{
    public interface ICustomerData
    {
        Task<Customer> PostCustomer(Customer c);
        Task<Guid> LoginCustomer(Customer c);
    }
}
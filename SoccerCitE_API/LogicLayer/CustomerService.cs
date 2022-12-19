using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using DataLayer;

namespace LogicLayer
{
    public class CustomerService : ICustomerService
    {
        // Dependency injection for data layer
        private readonly ICustomerData _iCustomerData;
        public CustomerService(ICustomerData iCustomerData)
        {
            this._iCustomerData = iCustomerData;
        }

        public async Task<Customer> PostCustomer(Customer c)
        {
            return await _iCustomerData.PostCustomer(c);
        }

        public async Task<Guid> LoginCustomer(Customer c)
        {
            return await _iCustomerData.LoginCustomer(c);
        }

        public async Task LogoutCustomer(Guid sessionId)
        {
            await _iCustomerData.LogoutCustomer(sessionId);   
        }
    }
}
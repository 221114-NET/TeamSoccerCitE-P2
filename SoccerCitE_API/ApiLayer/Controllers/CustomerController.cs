using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Import necessary layers
using ModelLayer;
using LogicLayer;

namespace ApiLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Dependency injection
        private readonly ICustomerService _iCustomerService;
        public CustomerController(ICustomerService iCustomerService) {
            this._iCustomerService = iCustomerService;
        }
        
        // Post User
        [HttpPost]
        public async Task<ActionResult<Customer>> RegisterCustomer(Customer customer) {
            try {
                customer = await _iCustomerService.PostCustomer(customer);
            } catch(Exception ex) {
                // Change to logger
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

            return customer;
        }

        // Get User
        [HttpPut]
        public async Task<ActionResult<Guid>> LoginCustomer(Customer customer)
        {
            return await _iCustomerService.LoginCustomer(customer);
        }
    }
}
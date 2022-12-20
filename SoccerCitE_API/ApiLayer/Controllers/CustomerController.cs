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
        private readonly IProductService _iProductService;
        public CustomerController(ICustomerService iCustomerService, IProductService iProductService) {
            this._iCustomerService = iCustomerService;
            this._iProductService = iProductService;
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

        //User Login
        [HttpPut("Login")]
        public async Task<ActionResult<Guid>> LoginCustomer(Customer customer)
        {
            return await _iCustomerService.LoginCustomer(customer);
        }

        //User Logout
        [HttpPut("Logout")]
        public async Task<ActionResult<string>> LogoutCustomer(Guid sessionId)
        {
            await _iCustomerService.LogoutCustomer(sessionId);
            return Ok("logout sucessful");
        }

        [HttpGet("UserProfile")]
        public async Task<ActionResult<object[]>> GetCustomerInfo(Guid sessionId) {
            // result array contains Customer data, and User Profile data
            object[] result = new object[2];
            var custmerData = await _iCustomerService.GetCustomerInfo(sessionId);
            result[0] = custmerData.Item1;
            result[1] = custmerData.Item2;
            return result;
        }

        [HttpPost("CartCheckout")] // Since PUT is idempotent, we use POST to keep removing stuff from db
        public async Task<ActionResult<string>> CartCheckout(List<Product> cart) {
            await _iProductService.CartCheckout(cart);
            return Ok("Checkout successful");
        }
    }
}
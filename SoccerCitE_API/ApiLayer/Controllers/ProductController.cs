using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LogicLayer;
using ModelLayer;

namespace ApiLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _iProductService;

        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductList()
        {
            return await _iProductService.GetProductList();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product newProduct) {
            try {
                return await _iProductService.PostProduct(newProduct);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
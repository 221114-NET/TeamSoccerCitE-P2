using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;
using DataLayer;

namespace LogicLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductData _iProductData;

        public ProductService(IProductData iProductData)
        {
            _iProductData = iProductData;
        }

        public async Task<List<Product>> GetProductList()
        {
            return await _iProductData.GetProductList();
        }

        async Task IProductService.CartCheckout(List<Product> cart)
        {
            await _iProductData.CartCheckout(cart);
        }
    }
}
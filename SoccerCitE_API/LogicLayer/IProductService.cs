using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;

namespace LogicLayer
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductList();
        public Task CartCheckout(List<Product> cart);
    }
}
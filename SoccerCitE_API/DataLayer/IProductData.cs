using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using ModelLayer;

namespace DataLayer
{
    public interface IProductData
    {
        public Task<List<Product>> GetProductList();
        public Task CartCheckout(List<Product> cart);
    }
}
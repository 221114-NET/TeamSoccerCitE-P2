using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public enum ProductCategory : int
    {
        SHOES = 1,
        BALLS, 
        JERSEYS
    }
    public class Product
    {
        public int ProductId{get; set;}
        public string Name{get; set;}
        public string Description{get; set;}
        public double Price{get; set;}
        public int Quantity{get; set;}
        public ProductCategory CategoryId{get; set;}
        // Last property is image, handle that later

        public Product() {}
        public Product(int productId, string name, string description, double price, int quantity, ProductCategory categoryId) {
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Quantity = quantity;
            this.CategoryId = categoryId;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MsgextActionSrchData.Model;

namespace MsgextActionSrchData.Controllers
{
    public class ProductController
    {
        public List<Product> GetAllProducts()
        {
            return new List<Product>() {
                new Product()
                {
                    Id = "1",
                    Name = "Product 1",
                    Orders = 1
                },
                new Product()
                {
                    Id = "2",
                    Name = "Product 2",
                    Orders = 1
                },
                new Product()
                {
                    Id = "3",
                    Name = "Product 3",
                    Orders = 1
                },
                new Product()
                {
                    Id = "4",
                    Name = "Product 4",
                    Orders = 1
                },
                new Product()
                {
                    Id = "5",
                    Name = "Product 5",
                    Orders = 1
                },
                new Product()
                {
                    Id = "6",
                    Name = "Product 6",
                    Orders = 1
                },
            };
        }
    }
}

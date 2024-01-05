using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using MsgextActionSrchData.Model;

namespace MsgextActionSrchData.Controllers
{
    public class ProductController
    {
        private TableClient tableClient;
        public ProductController(IConfiguration config)
        {
            string accountName = config["AZURE_TABLE_ACCOUNTNAME"];
            string storageAccountKey = config["AZURE_TABLE_KEY"];
            string storageUrl = $"https://{accountName}.table.core.windows.net/";
            tableClient = new TableClient(new Uri(storageUrl), "Products", new TableSharedKeyCredential(accountName, storageAccountKey));
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            Pageable<TableEntity> list = tableClient.Query<TableEntity>();
            foreach (TableEntity item in list)
            {
                Product p = new Product()
                {
                    Id = item.PartitionKey,
                    Name = item.RowKey,
                    Orders = (int)item.GetInt32("Orders"),
                    Orderable = (bool)item.GetBoolean("Orderable")
                };
                products.Add(p);
            }
            return products;
        }
        public List<Product> GetAllMockProducts()
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

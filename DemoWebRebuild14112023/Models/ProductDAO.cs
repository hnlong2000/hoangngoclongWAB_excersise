using System.Diagnostics;

namespace DemoWebRebuild14112023.Models
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>() 
            {
                new Product { Id = 1, Name="Laptop Acer" , Description="Laptop", Price= 1200300.500M},
                new Product { Id = 2, Name="Laptop Dell" , Description="Laptop", Price= 1200500.500M},
                new Product { Id = 3, Name="Bàn phím Acer" , Description="Bàn phím", Price= 1000300.500M},
                new Product { Id = 4, Name="Chuột Acer" , Description="Chuột", Price= 1000400.500M},

            };
            return products;
        }
    }
}

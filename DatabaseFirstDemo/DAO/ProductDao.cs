using DatabaseFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo.DAO
{
    public class ProductsDao
    {
        private static ProductsDao instance;
        private static readonly object instanceLock = new object();
        public static ProductsDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductsDao();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetAll()
        {
            List<Product> product;
            try
            {
                using Batch177179Context stock = new Batch177179Context();
                product = stock.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
            Commit();
        }

        public Product Find(string Id)
        {
            Product product = products.Find(c => c.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(c => c.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
                Commit();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(c => c.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
                Commit();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}

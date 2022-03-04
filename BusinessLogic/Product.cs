using RateLimit_WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit_WebAPI
{
    public partial class Product
    {
        private readonly DatabaseContext _databaseContext;

        public Product(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        public List<Product> GetProducts()
        {
            try
            {
                List<Product> products = _databaseContext.Products.ToList();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        public Product GetProductbyId(int id)
        {
            try
            {
                Product product = _databaseContext.Products.Find(id);
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        public int AddProduct(Product product)
        {
            try
            {
                _databaseContext.Products.Add(product);
                _databaseContext.SaveChanges();
                return product.ProductId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        public int UpdateProduct(Product product, int Id)
        {
            try
            {
                Product FindProduct = _databaseContext.Products.FirstOrDefault(a => a.ProductId == Id);
                if (FindProduct == null)
                    return 0;
                FindProduct.Description = product.Description;
                FindProduct.Name = product.Name;
                FindProduct.Price = product.Price;

                //_databaseContext.Update(FindProduct);
                _databaseContext.SaveChanges();
                return this.ProductId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        public bool DeleteProduct(int id)
        {
            try
            {
                bool isDeleted = false;
                Product product = _databaseContext.Products.Find(id);
                if (product != null)
                {
                    _databaseContext.Remove(product);
                    _databaseContext.SaveChanges();
                    isDeleted = true;
                }

                return isDeleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}

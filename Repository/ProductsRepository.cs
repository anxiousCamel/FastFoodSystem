using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;
using EasyProduct.Data;

namespace EasyProduct.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        // Construtor BancoContext
        private readonly BancoContext _BancoContext;
        public ProductsRepository(BancoContext bancoContext)
        {
            _BancoContext = bancoContext;

        }

        // Searche products
        public List<ProductsModel> SearcheAll()
        {
            return _BancoContext.Products.ToList();
        }

        // Add products
        public ProductsModel AddProduct(ProductsModel products)
        {
            _BancoContext.Products.Add(products);
            _BancoContext.SaveChanges();

            return products;
        }
    }
}
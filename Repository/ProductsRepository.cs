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

        // Buscar por id
        public ProductsModel SearcheForId(int id)
        {
            return _BancoContext.Products.FirstOrDefault(x => x.Id == id) ?? new ProductsModel();
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

        public ProductsModel EditProduct(ProductsModel products)
        {
            ProductsModel productDB = SearcheForId(products.Id);
            if (productDB == null) throw new Exception("There was an error to edit, this product does not exist");
            
            productDB.Name = products.Name;
            productDB.Ingredients = products.Ingredients;
            productDB.Description = products.Description;
            productDB.Price = products.Price;
            productDB.PromotionPrice = products.PromotionPrice;
            productDB.DayPromotion = products.DayPromotion;
            productDB.ImageURL = products.ImageURL;
            productDB.Type = products.Type;

            _BancoContext.Products.Update(productDB);
            _BancoContext.SaveChanges();

            return productDB;
        }

        public bool DeleteProduct(int Id)
        {
            ProductsModel productDB = SearcheForId(Id);
            if (productDB == null) throw new Exception("There was an error to delete, this product does not exist");

            _BancoContext.Products.Remove(productDB);
            _BancoContext.SaveChanges();

            return true;
        }
    }
}
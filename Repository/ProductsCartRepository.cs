using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;
using EasyProduct.Data;
using Microsoft.AspNetCore.Http;

namespace EasyProduct.Repository
{
    public class ProductsCartRepository : IProductsCartRepository
    {
        private readonly BancoContext _BancoContext;
        private readonly IProductsRepository _productsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductCartModel SearcheForId(int id)
        {
            return _BancoContext.ProductCarts.FirstOrDefault(x => x.Id == id) ?? new ProductCartModel();
        }

        public ProductsCartRepository(BancoContext bancoContext, IProductsRepository productsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _BancoContext = bancoContext;
            _productsRepository = productsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public ProductCartModel AddToCart(ProductCartModel cartItem)
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext != null)
            {
                var selectedIngredients = httpContext.Request.Form["model.SelectedIngredients"];
                var selectedAdditionalProducts = httpContext.Request.Form["model.SelectedAdditionalProductsId"];

                if (selectedIngredients.Count > 0)
                {
                    // Concatene os IDs dos produtos adicionais em uma única string separada por vírgulas
                    cartItem.SelectedAdditionalProductsId = string.Join(",", selectedAdditionalProducts);
                    cartItem.SelectedIngredients = string.Join(",", selectedIngredients);
                }

                _BancoContext.ProductCarts.Add(cartItem);
                _BancoContext.SaveChanges();
            }

            return cartItem;
        }
        public List<ProductCartModel> GetCartItems()
        {
            return _BancoContext.ProductCarts.ToList();
        }

        public ProductCartModel EditProductCart(ProductCartModel products)
        {
            ProductCartModel productDB = SearcheForId(products.Id);
            if (productDB == null) throw new Exception("There was an error to edit, this product does not exist");

            productDB.Quantity = products.Quantity;
            productDB.SelectedIngredients = products.SelectedIngredients;
            productDB.SelectedAdditionalProductsId = products.SelectedAdditionalProductsId;

            _BancoContext.ProductCarts.Update(productDB);
            _BancoContext.SaveChanges();

            return productDB;
        }

        public bool RemoveToCart(int Id)
        {
            ProductCartModel productDB = SearcheForId(Id);
            if (productDB == null) throw new Exception("There was an error to delete, this product does not exist");
            
            _BancoContext.ProductCarts.RemoveRange(productDB);
            _BancoContext.SaveChanges();

            return true;
        }

    }
}

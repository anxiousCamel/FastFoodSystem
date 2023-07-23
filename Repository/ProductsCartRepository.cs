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
        public ProductsCartRepository(BancoContext bancoContext, IProductsRepository productsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _BancoContext = bancoContext;
            _productsRepository = productsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public ProductCartModel SearcheForId(int id)
        {
            return _BancoContext.ProductCarts.FirstOrDefault(x => x.Id == id) ?? new ProductCartModel();
        }

        public ProductCartModel GetProductInfo(int productId)
        {
            var productCart = SearcheForId(productId);

            // Verificar se o produto foi encontrado
            if (productCart == null)
            {
                throw new Exception("There was an error to edit, this product does not exist");
            }

            // Mapear as informações do produto para o modelo ProductCartModel
            var productInfo = new ProductCartModel
            {
                ProductId = productCart.ProductId,
                SelectedAdditionalProductsId = productCart.SelectedAdditionalProductsId,
                SelectedIngredients = productCart.SelectedIngredients
            };

            return productInfo;
        }


        public ProductCartModel AddToCart(ProductCartModel cartItem)
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext != null)
            {

                var selectedIngredients = httpContext.Request.Form["model.SelectedIngredients"];
                if (selectedIngredients.Count > 0)
                { cartItem.SelectedIngredients = string.Join(",", selectedIngredients); }

                var selectedAdditionalProducts = httpContext.Request.Form["model.SelectedAdditionalProductsId"];
                if (selectedAdditionalProducts.Count > 0)
                { cartItem.SelectedAdditionalProductsId = string.Join(",", selectedAdditionalProducts); }

                _BancoContext.ProductCarts.Add(cartItem);
                _BancoContext.SaveChanges();
            }

            return cartItem;
        }
        public List<ProductCartModel> GetCartItems()
        {
            return _BancoContext.ProductCarts.ToList();
        }

        public ProductCartModel EditProduct(ProductCartModel product)
        {
            ProductCartModel productDB = SearcheForId(product.Id);
            if (productDB == null)
            {
                throw new Exception("There was an error editing the product. The product does not exist.");
            }

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext != null)
            {
                var selectedIngredients = httpContext.Request.Form["SelectedIngredients"];
                if (selectedIngredients.Count > 0)
                { product.SelectedIngredients = string.Join(",", selectedIngredients); }

                var selectedAdditionalProducts = httpContext.Request.Form["SelectedAdditionalProductsId"];
                if (selectedAdditionalProducts.Count > 0)
                { product.SelectedAdditionalProductsId = string.Join(",", selectedAdditionalProducts); }
            }

            productDB.Quantity = product.Quantity;
            productDB.SelectedIngredients = product.SelectedIngredients;
            productDB.SelectedAdditionalProductsId = product.SelectedAdditionalProductsId;

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

        public bool RemoveAllToCart()
        {
            var products = _BancoContext.ProductCarts.ToList();
            _BancoContext.ProductCarts.RemoveRange(products);
            _BancoContext.SaveChanges();

            return true;
        }

        public double CalculateTotalCartPrice()
        {
            var totalCartPrice = 0.0;
            var cartItems = GetCartItems();
            foreach (var cartItem in cartItems)
            {
                var product = _productsRepository.SearcheForId(cartItem.ProductId);
                if (product != null)
                {
                    var additionalProductsTotalPrice = 0.0;
                    if (!string.IsNullOrEmpty(cartItem.SelectedAdditionalProductsId))
                    {
                        var additionalProductIds = cartItem.SelectedAdditionalProductsId.Split(',');
                        foreach (var additionalProductId in additionalProductIds)
                        {
                            var selectedProduct = _productsRepository.SearcheForId(int.Parse(additionalProductId));
                            if (selectedProduct != null)
                            {
                                additionalProductsTotalPrice += selectedProduct.Price;
                            }
                        }
                    }

                    double totalPrice;
                    if (product.DayPromotion == (int)DateTime.Now.DayOfWeek || product.DayPromotion == 8)
                    {
                        totalPrice = (product.PromotionPrice + additionalProductsTotalPrice) * cartItem.Quantity;
                    }

                    else
                    {
                        totalPrice = (product.Price + additionalProductsTotalPrice) * cartItem.Quantity;
                    }
                    
                    totalCartPrice += totalPrice;
                }
            }
            return totalCartPrice;
        }
    }
}

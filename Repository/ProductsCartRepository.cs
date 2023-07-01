using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;
using EasyProduct.Data;

namespace EasyProduct.Repository
{
    public class ProductsCartRepository : IProductsCartRepository
    {
        private static List<ProductCartModel> _cartItems = new List<ProductCartModel>();
        private readonly IProductsRepository _productsRepository;

        public ProductsCartRepository(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public void AddToCart(int productId, List<int> selectedIngredients, List<int> selectedAdditionalProducts, int quantity)
        {
            var product = _productsRepository.SearcheForId(productId);

            var cartItem = new ProductCartModel
            {
                ProductId = productId, // Armazena apenas o ID do produto
                SelectedAdditionalProductsId = string.Join(",", selectedAdditionalProducts),
                SelectedIngredients = string.Join(",", selectedIngredients),
                Quantity = quantity
            };
            _cartItems.Add(cartItem);
        }

        public List<ProductCartModel> GetCartItems()
        {
            var cartItemsWithProducts = new List<ProductCartModel>();

            foreach (var cartItem in _cartItems)
            {
                var product = _productsRepository.SearcheForId(cartItem.ProductId);
                if (product != null)
                {
                    var cartItemWithProduct = new ProductCartModel
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        SelectedIngredients = cartItem.SelectedIngredients,
                        SelectedAdditionalProductsId = cartItem.SelectedAdditionalProductsId,
                        Ingredients = cartItem.Ingredients,
                        Quantity = cartItem.Quantity
                    };

                    cartItemsWithProducts.Add(cartItemWithProduct);
                }
            }

            return cartItemsWithProducts;
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}

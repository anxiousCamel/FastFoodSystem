using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;

namespace EasyProduct.Repository.Interface
{
    public interface IProductsCartRepository
    {
        void AddToCart(int productId, List<int> selectedIngredients, List<int> selectedAdditionalProducts, int quantity);
        List<ProductCartModel> GetCartItems();
        void ClearCart();
    }
}
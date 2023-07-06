using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;

namespace EasyProduct.Repository.Interface
{
    public interface IProductsCartRepository
    {
        List<ProductCartModel> GetCartItems();
        ProductCartModel SearcheForId (int id);
        ProductCartModel AddToCart(ProductCartModel cartItem);
        ProductCartModel EditProduct(ProductCartModel product);
        ProductCartModel GetProductInfo(int productId);
        bool RemoveToCart(int Id);
    }
}
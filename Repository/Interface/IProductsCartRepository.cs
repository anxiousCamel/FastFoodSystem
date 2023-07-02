using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;

namespace EasyProduct.Repository.Interface
{
    public interface IProductsCartRepository
    {
        ProductCartModel SearcheForId (int id);
        ProductCartModel AddToCart(ProductCartModel cartItem);
        List<ProductCartModel> GetCartItems();
        bool ClearCart(int Id);
    }
}
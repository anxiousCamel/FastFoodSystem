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
        double CalculateTotalCartPrice();
        PaymentModel AddPayment(PaymentModel products);
        ProductCartModel AddToCart(ProductCartModel cartItem);
        ProductCartModel EditProduct(ProductCartModel product);
        bool RemoveToCart(int Id);
        bool RemoveAllToCart();
        void FindCombosInCart();
    }
}
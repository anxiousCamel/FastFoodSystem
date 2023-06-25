using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;

namespace EasyProduct.Repository.Interface
{
    public interface IProductsRepository
    {
        ProductsModel SearcheForId (int id);
        List<ProductsModel> SearcheAll ();
        ProductsModel AddProduct (ProductsModel product);
    }
}
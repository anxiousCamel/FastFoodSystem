using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyProduct.Models
{
    public class ProductCartModel
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? SelectedIngredients { get; set; }
        public string? SelectedAdditionalProductsId { get; set; }
        public int Quantity { get; set; }
    }
}
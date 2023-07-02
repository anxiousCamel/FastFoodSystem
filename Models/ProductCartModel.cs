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
        public string SelectedIngredients { get; set; } = null!;
        public string SelectedAdditionalProductsId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
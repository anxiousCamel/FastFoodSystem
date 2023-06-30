using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyProduct.Models
{
    public class ProductCartModel
    {
        public int ProductId { get; set; }
        public List<ProductsModel> Products { get; set; } = null!;
        public ProductsModel Product { get; set; } = null!;
        public List<int> SelectedIngredients { get; set; } = null!;
        public List<int> SelectedAdditionalProducts { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public int Quantity { get; set; }

    }
}
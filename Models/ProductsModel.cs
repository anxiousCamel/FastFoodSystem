using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyProduct.Models
{
    public class ProductsModel
    {
        public int Id { get; set; }
        public String Name { get; set; } = null!;
        public String Ingredients { get; set; } = null!;
        public String Description { get; set; } = null!;
        public float Price { get; set; }
        public float PromotionPrice { get; set; }
        public int DayPromotion { get; set; }
        public String ImageURL { get; set; } = null!;
        public int Type { get; set; }
    }
}
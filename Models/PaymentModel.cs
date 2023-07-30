using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyProduct.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? IngredientsAndAdditional { get; set; }
        public int Quantity { get; set; }
        public String Name { get; set; } = null!;
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        public string? ConditionPayment { get; set; }
        public int ConditionKitchen { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public int Type { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyProduct.Models
{
    public class GroupModel
    {
        public List<ProductsModel> Products { get; set; } = null!;
        public List<ProductCartModel> Cart { get; set; } = null!;

    }
}
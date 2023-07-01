using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyProduct.Data
{
    public class BancoContext : DbContext
    {   
        public BancoContext(DbContextOptions <BancoContext> options) : base(options)
        {

        }

        public DbSet <ProductsModel> Products { get; set; }
        public DbSet<ProductCartModel> ProductCarts { get; set; }
    }
}
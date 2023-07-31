using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Repository.Interface;
using EasyProduct.Models;
using EasyProduct.Data;

namespace EasyProduct.Repository.Interface
{
    public interface IDashboardRepository
    {
        PaymentModel SearcheForId (int id);
        List<PaymentModel> SearcheAll ();
    }
}
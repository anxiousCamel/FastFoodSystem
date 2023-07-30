using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;

namespace EasyProduct.Repository.Interface
{
    public interface IScreensRepository
    {
        PaymentModel SearcheForId (int id);
        List<PaymentModel> SearcheAll ();
        void UpdateConditionKitchen(int id, int newConditionKitchenValue);
    }
}
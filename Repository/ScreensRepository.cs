using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;
using EasyProduct.Data;

namespace EasyProduct.Repository
{
    public class ScreensRepository : IScreensRepository
    {
        private readonly BancoContext _BancoContext;
        public ScreensRepository(BancoContext bancoContext)
        {
            _BancoContext = bancoContext;

        }

        public PaymentModel SearcheForId(int id)
        {
            return _BancoContext.PaymentInfo.FirstOrDefault(x => x.Id == id) ?? new PaymentModel();
        }

        // Searche products
        public List<PaymentModel> SearcheAll()
        {
            return _BancoContext.PaymentInfo.ToList();
        }

        public void UpdateConditionKitchen(int id, int newConditionKitchenValue)
        {
            var paymentModel = SearcheForId(id);
            if (paymentModel != null)
            {
                paymentModel.ConditionKitchen = newConditionKitchenValue;
                paymentModel.PreparationTime = DateTime.Now - paymentModel.DateTime;
                _BancoContext.SaveChanges();
            }
        }
    }
}
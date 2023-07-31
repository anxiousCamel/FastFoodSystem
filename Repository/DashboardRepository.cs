using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyProduct.Repository.Interface;
using EasyProduct.Models;
using EasyProduct.Data;

namespace EasyProduct.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly BancoContext _BancoContext;
        public DashboardRepository(BancoContext bancoContext)
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

    }
}
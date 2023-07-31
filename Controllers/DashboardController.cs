using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;


namespace EasyProduct.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _DashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _DashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            List<PaymentModel> dashboard = _DashboardRepository.SearcheAll();
            return View(dashboard);
        }
    }
}
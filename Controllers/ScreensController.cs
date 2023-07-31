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
    public class ScreensController : Controller
    {
        private readonly IScreensRepository _ScreensRepository;
        public ScreensController(IScreensRepository screensRepository)
        {
            _ScreensRepository = screensRepository;
        }

        public IActionResult Index()
        {
            List<PaymentModel> screens = _ScreensRepository.SearcheAll();
            return View(screens);
        }

        [HttpPost]
        [Route("UpdateConditionKitchen")]
        public IActionResult UpdateConditionKitchen(int id)
        {
            // Call the repository method to update the ConditionKitchen to 5
            _ScreensRepository.UpdateConditionKitchen(id, 5);

            // Redirect back to the Index action
            return RedirectToAction("Index");
        }

    }
}
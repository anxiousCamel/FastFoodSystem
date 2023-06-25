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
    public class ManagerController : Controller
    {
        // Construtor IProductsRepository
        private readonly IProductsRepository _ProductsRepository;
        public ManagerController(IProductsRepository productsRepository)
        {
            _ProductsRepository = productsRepository;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductsModel products)
        {
            _ProductsRepository.AddProduct(products);
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Index()
        {
            List<ProductsModel> products = _ProductsRepository.SearcheAll();
            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
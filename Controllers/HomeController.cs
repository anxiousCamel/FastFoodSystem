using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;

namespace EasyProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IProductsCartRepository _productsCartRepository;

        public HomeController(IProductsRepository productsRepository, IProductsCartRepository productsCartRepository)
        {
            _productsRepository = productsRepository;
            _productsCartRepository = productsCartRepository;
        }

        public IActionResult Index()
        {
            List<ProductsModel> products = _productsRepository.SearcheAll();
            List<ProductCartModel> cartItems = _productsCartRepository.GetCartItems();

            var viewModel = new GroupModel
            {
                Products = products,
                Cart = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(ProductCartModel model)
        {
            _productsCartRepository.AddToCart(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            List<ProductCartModel> cartItems = _productsCartRepository.GetCartItems();

            if (cartItems.Any())
            {
                return PartialView("_CartItems", cartItems);
            }
            else
            {
                return PartialView("_EmptyCart");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductCartModel products)
        {
            ProductCartModel edited = _productsCartRepository.EditProductCart(products);
            return Json(new { edited = edited });
        }

        public IActionResult RemoveToCart (int id)
        {
            bool removed = _productsCartRepository.RemoveToCart(id);
            return Json(new { removed = removed });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
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
            // Obter todos os produtos
            List<ProductsModel> products = _productsRepository.SearcheAll();

            // Obter todos os itens do carrinho
            List<ProductCartModel> cartItems = _productsCartRepository.GetCartItems();

            // Encontrar os combos no carrinho (não precisa atribuir a uma variável)
            _productsCartRepository.FindCombosInCart();

            // Definir a propriedade ComboItem em cada item do carrinho (cartItem)
            foreach (var cartItem in cartItems)
            {
                cartItem.ComboItem = cartItem.ComboItem; // Não é necessário, mas mantido para referência
            }

            var viewModel = new GroupModel
            {
                Products = products,
                Cart = cartItems
            };

            // Não é necessário adicionar comboItems à ViewBag, pois o método já atualizou os ComboItems nos cartItems

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetTotalPrice()
        {
            double totalCartPrice = _productsCartRepository.CalculateTotalCartPrice();
            return Json(new { totalPrice = totalCartPrice });
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
            return PartialView(cartItems);
        }

        [HttpPost]
        public IActionResult Edit(ProductCartModel product)
        {
            _productsCartRepository.EditProduct(product);
            return RedirectToAction("Index");
        }


        public IActionResult Remove(int id)
        {
            bool removido = _productsCartRepository.RemoveToCart(id);
            return RedirectToAction("Index");
        }


        public IActionResult RemoveAllToCart()
        {
            _productsCartRepository.RemoveAllToCart();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
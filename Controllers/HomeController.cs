using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;

namespace EasyProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private static List<ProductCartModel> _cartItems = new List<ProductCartModel>();

        public HomeController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            List<ProductsModel> products = _productsRepository.SearcheAll();

            var viewModel = new ProductCartModel
            {
                Products = products
            };

            return View(viewModel); // Passando o modelo correto para a View
        }

        [HttpPost]
        public IActionResult AddToCart(ProductCartModel model)
        {
            // Acessar as propriedades do formulário
            List<int> selectedIngredients = model.SelectedIngredients;
            string ingredients = model.Ingredients;
            int quantity = model.Quantity;

            // Processar os dados do formulário e adicionar ao carrinho
            var product = _productsRepository.SearcheForId(model.ProductId);
            var cartItem = new ProductCartModel
            {
                Product = product,
                SelectedIngredients = selectedIngredients,
                Ingredients = ingredients,
                Quantity = quantity
            };
            _cartItems.Add(cartItem);

            // Atualizar a exibição do carrinho
            // Você pode passar os dados do carrinho para a mesma página ou utilizar AJAX para atualizar somente o off-canvas

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            return PartialView("_CartItems", _cartItems);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

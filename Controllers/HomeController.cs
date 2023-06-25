using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyProduct.Models;
using EasyProduct.Repository.Interface;

namespace EasyProduct.Controllers;

public class HomeController : Controller
{
    // Construtor IProductsRepository
    private readonly IProductsRepository _ProductsRepository;
    public HomeController(IProductsRepository productsRepository)
    {
        _ProductsRepository = productsRepository;
    }

    public IActionResult Index()
    {
        List <ProductsModel> products = _ProductsRepository.SearcheAll();
        return View(products);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

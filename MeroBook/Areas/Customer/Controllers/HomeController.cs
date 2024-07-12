using MeroBook.DataAccess.Repository;
using MeroBook.DataAccess.Repository.IRepository;
using MeroBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MeroBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMeroBookRepository _meroBookRepo;

        public HomeController(ILogger<HomeController> logger, IMeroBookRepository meroBookRepo)
        {
            _logger = logger;
            _meroBookRepo = meroBookRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _meroBookRepo.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            Product product = _meroBookRepo.Product.Get(u=>u.Id== productId, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

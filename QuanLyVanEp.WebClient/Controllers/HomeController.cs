using ApiDocs;
using Microsoft.AspNetCore.Mvc;
using QuanLyVanEp.WebClient.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(DataClient context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
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

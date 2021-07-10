using ApiDocs;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(DataClient client) : base(client)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

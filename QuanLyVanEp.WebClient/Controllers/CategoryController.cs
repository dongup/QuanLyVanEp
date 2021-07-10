using ApiDocs;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(DataClient client) : base(client)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

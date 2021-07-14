using ApiDocs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

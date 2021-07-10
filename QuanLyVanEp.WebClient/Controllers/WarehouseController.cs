using ApiDocs;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class WarehouseController : BaseController
    {
        public WarehouseController(DataClient client) : base(client)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

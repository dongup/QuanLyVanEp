using ApiDocs;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(DataClient client) : base(client)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

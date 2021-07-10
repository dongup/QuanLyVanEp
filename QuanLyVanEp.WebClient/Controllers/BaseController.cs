using ApiDocs;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVanEp.WebClient.Controllers
{
    public class BaseController : Controller
    {
        protected DataClient _client;

        public BaseController(DataClient dataClient)
        {
            _client = dataClient;
        }
    }
}

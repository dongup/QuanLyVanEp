namespace QuanLyVanEp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected AppDbContext _context;

        public BaseApiController(AppDbContext context)
        {
            _context = context;
        }
    }
}
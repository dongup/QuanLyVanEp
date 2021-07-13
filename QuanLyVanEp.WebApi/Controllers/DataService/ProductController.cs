using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using QuanLyVanEp.DataAccess.DAL;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IWebHostEnvironment _webEnv;

        public ProductController(AppDbContext context, UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment) : base(context, userManager)
        {
            _webEnv = hostingEnvironment;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            var res = await _context.Products.ToListAsync();

            rspns.Succeed(res);
            return rspns;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetById(int id)
        {
            var entity = await _context.Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null) return rspns.NotFound();

            return rspns.Succeed(entity);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(ProductRequest value)
        {
            ProductEntity newItem = new ProductEntity();
            newItem.ProductName = value.ProductName;
            newItem.ProductCode = value.ProductCode;
            newItem.Desciption = value.Desciption;
            newItem.CategoryId = value.CategoryId;
            newItem.CategoryName = _context.ProductCategories.Find(value.CategoryId)?.Name;

            _context.Products.Add(newItem);
            _context.SaveChanges();
            return rspns.Succeed();
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, ProductRequest value)
        {
            var item = _context.Products.Find(id);
            if (item == null) return rspns.NotFound();
            item.ProductName = value.ProductName;
            item.ProductCode = value.ProductCode;
            item.Desciption = value.Desciption;
            item.CategoryId = value.CategoryId;
            item.CategoryName = _context.ProductCategories.Find(value.CategoryId)?.Name;

            _context.SaveChanges();
            return rspns;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
            {
                rspns.NotFound();
                return rspns;
            }

            _context.Products.Remove(entity);
            _context.SaveChanges();

            rspns.Succeed();
            return rspns;
        }
    }
}

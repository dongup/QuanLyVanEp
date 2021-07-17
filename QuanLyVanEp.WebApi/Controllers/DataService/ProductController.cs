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
            var res = await _context.Products
                .Include(p => p.ProductCategory)
                .Select(p => new ProductEntity() { 
                    ProductName = p.ProductName,
                    ProductCode = p.ProductCode,
                    ProductPrice = p.ProductPrice,
                    CreatedDate = p.CreatedDate,
                    CategoryName = p.CategoryName,
                    Unit = p.Unit,
                    Id = p.Id
                })
                .ToListAsync();

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
            var found = await _context.Products.AnyAsync(p => (p.ProductName == value.ProductName || p.ProductCode == value.ProductCode)  && p.IsDeleted == false );
            if (!found)
            {
                newItem.ProductName = value.ProductName;
                newItem.ProductCode = value.ProductCode;
                newItem.ProductPrice = value.ProductPrice;
                newItem.Desciption = value.Desciption;
                newItem.CategoryId = value.CategoryId;
                newItem.Unit = value.Unit;
                newItem.CategoryName = _context.ProductCategories.Find(value.CategoryId)?.Name;

                _context.Products.Add(newItem);
                await _context.SaveChangesAsync();

                rspns.Succeed();
            }
            else
            {
                rspns.Failed("Tên hoặc mã sản phẩm đã tồn tại");
            }
            
            return rspns;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, ProductRequest value)
        {
            var found = await _context.Products.AnyAsync(p => (p.ProductName == value.ProductName || p.ProductCode == value.ProductCode) && p.IsDeleted == false && p.Id != id);
            if (!found)
            {
                var item = _context.Products.Find(id);
                if (item == null) 
                    return rspns.NotFound();

                item.ProductName = value.ProductName;
                item.ProductCode = value.ProductCode;
                item.ProductPrice = value.ProductPrice;
                item.CategoryId = value.CategoryId;
                item.Unit = value.Unit;
                item.CategoryName = _context.ProductCategories.Find(value.CategoryId)?.Name;

                await _context.SaveChangesAsync();
                rspns.Succeed();
            }
            else
            {
                rspns.Failed("Tên sản phẩm đã tồn tại");
            }
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

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DTO;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using TaiyoshaEPE.DataAccess.Models.General;
using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputsController : BaseController
    {
        IWebHostEnvironment _webEnv;

        public OutputsController(AppDbContext context, UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment) : base(context, userManager)
        {
            _webEnv = hostingEnvironment;
        }

        // GET: api/Outputs
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            var res = await _context.Outputs
                .Include(op => op.Product)
                .Select(op => new { 
                    Id = op.Id,
                    ProductName = op.Product.ProductName,
                    OutputNumber = op.OutputNumber,
                    OutputPrice = op.OutputPrice,
                    CreatedDate = op.CreatedDate
                })
                .ToListAsync();

            rspns.Succeed(res);

            return rspns;
        }

        // GET: api/Outputs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetById(int id)
        {
            var entity = await _context.Outputs
                .Where(x => x.Id == id)
                .Select(x => new OutputReponse(x))
                .FirstOrDefaultAsync();
            if (entity == null) return rspns.NotFound();

            return rspns.Succeed(entity);
        }

        // POST: api/Outputs
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(OutputReponse value)
        {
            var product = _context.Products.Find(value.ProductId);
            if (product.StockNumber > 0)
            {
                OutputEntity newItem = new OutputEntity();

                newItem.ProductId = value.ProductId;
                newItem.OutputNumber = value.OutputNumber;
                newItem.OutputPrice = newItem.OutputNumber * product.ProductPrice;
                _context.Outputs.Add(newItem);
                await _context.SaveChangesAsync();

                product.StockNumber -= value.OutputNumber;
                product.SoldNumber += value.OutputNumber;
                await _context.SaveChangesAsync();

                rspns.Succeed();
            }
            else
            {
                rspns.Failed("Kiểm tra số lượng sản phẩm trong kho");
            }

            return rspns;
        }

        // PUT: api/Outputs/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, OutputReponse value)
        {
            var product = _context.Products.Find(value.ProductId);
            if (product.StockNumber > 0)
            {
                var entity = _context.Outputs.Find(id);
                if (entity == null) 
                    return rspns.NotFound();

                entity.ProductId = value.ProductId;
                entity.OutputNumber = value.OutputNumber;
                entity.OutputPrice = value.OutputPrice * product.ProductPrice;
                await _context.SaveChangesAsync();
                
                var oriOutput = entity.OutputNumber;
                product.StockNumber += oriOutput;
                product.StockNumber -= value.OutputNumber;
                product.SoldNumber -= oriOutput;
                product.SoldNumber += value.OutputNumber;
                await _context.SaveChangesAsync();
                rspns.Succeed();
            }
            else
            {
                rspns.Failed("Kiểm tra số lượng sản phẩm trong kho");
            }

            return rspns;
        }

        // DELETE: api/Outputs/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            var entity = await _context.Outputs.FindAsync(id);
            if (entity == null)
            {
                rspns.NotFound();
                return rspns;
            }

            _context.Outputs.Remove(entity);
            _context.SaveChanges();

            var product = _context.Products.Find(entity.ProductId);
            product.StockNumber += entity.OutputNumber;
            product.SoldNumber -= entity.OutputNumber;
            _context.SaveChanges();

            rspns.Succeed();
            return rspns;
        }
    }
}

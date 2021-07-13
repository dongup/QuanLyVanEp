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
                .Select(x => new OutputReponse(x))
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
            OutputEntity entity = new OutputEntity();
            ModelUtils.CopyProperty(value, entity);
            _context.Outputs.Add(entity);
            _context.SaveChanges();

            var product = _context.Products.Find(value.ProductId);
            product.StockNumber -= value.OutputNumber;
            product.SoldNumber += value.OutputNumber;
            _context.SaveChanges();

            return rspns.Succeed();
        }

        // PUT: api/Outputs/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, OutputReponse value)
        {
            var entity = _context.Outputs.Find(id);
            if (entity == null) return rspns.NotFound();
            var oriOutput = entity.OutputNumber;

            ModelUtils.CopyProperty(value, entity);
            _context.SaveChanges();

            var product = _context.Products.Find(value.ProductId);
            product.StockNumber += oriOutput;
            product.StockNumber -= value.OutputNumber;
            product.SoldNumber -= oriOutput;
            product.SoldNumber += value.OutputNumber;
            _context.SaveChanges();

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

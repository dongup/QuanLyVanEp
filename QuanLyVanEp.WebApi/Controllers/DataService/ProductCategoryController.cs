﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using QuanLyVanEp.DataAccess.DAL;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseController
    {
        IWebHostEnvironment _webEnv;

        public ProductCategoryController(AppDbContext context, UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment) : base(context, userManager)
        {
            _webEnv = hostingEnvironment;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            var res = await _context.ProductCategories.ToListAsync();

            return rspns.Succeed(res);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetById(int id)
        {
            var entity = await _context.ProductCategories
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (entity == null) return rspns.NotFound();

            return rspns.Succeed(entity);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(ProductCatergoryRequest value)
        {
            ProductCategoryEntity newItem = new ProductCategoryEntity();
            newItem.Name = value.Name;

            _context.ProductCategories.Add(newItem);
            await _context.SaveChangesAsync();
            return rspns.Succeed();
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, ProductCatergoryRequest value)
        {
            var entity = _context.ProductCategories.Find(id);
            if (entity == null) return rspns.NotFound();
            entity.Name = value.Name;

            await _context.SaveChangesAsync();
            return rspns.Succeed();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            var entity = await _context.ProductCategories.FindAsync(id);
            if (entity == null)
            {
                rspns.NotFound();
                return rspns;
            }

            _context.ProductCategories.Remove(entity);
            _context.SaveChanges();

            rspns.Succeed();
            return rspns;
        }
    }
}
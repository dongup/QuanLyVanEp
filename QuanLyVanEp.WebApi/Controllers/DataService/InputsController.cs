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
    public class InputsController : BaseController
    {
        IWebHostEnvironment _webEnv;

        public InputsController(AppDbContext context, UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment) : base(context, userManager)
        {
            _webEnv = hostingEnvironment;
        }

        // GET: api/Inputs
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            var res = await _context.Inputs
                .Select(x => new InputResponse(x))
            .ToListAsync();

            rspns.Succeed(res);

            return rspns;
        }

        // GET: api/Inputs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetById(int id)
        {
            var entity = await _context.Inputs
                .Where(x => x.Id == id)
                .Select(x => new InputResponse(x) { 
                    Outputs = x.Outputs.Select(x => new OutputReponse(x)).ToList()
                })
                .FirstOrDefaultAsync();
            if (entity == null) return rspns.NotFound();

            return rspns.Succeed(entity);
        }

       
        // POST: api/Inputs
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(InputResponse value)
        {
            InputEntity entity = new InputEntity();
            ModelUtils.CopyProperty(value, entity);
            _context.Inputs.Add(entity);
            _context.SaveChanges();
            return rspns.Succeed();
        }

        // PUT: api/Inputs/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> Put(int id, InputResponse value)
        {
            var entity = _context.Inputs.Find(id);
            if (entity == null) return rspns.NotFound();
            ModelUtils.CopyProperty(value, entity);
            _context.SaveChanges();
            return rspns;
        }

        // DELETE: api/Inputs/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            var entity = await _context.Inputs.FindAsync(id);
            if (entity == null)
            {
                rspns.NotFound();
                return rspns;
            }

            _context.Inputs.Remove(entity);
            rspns.Succeed();
            return rspns;
        }
    }
}

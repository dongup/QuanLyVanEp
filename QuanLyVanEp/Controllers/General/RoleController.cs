using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.DTO;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public RoleController(AppDbContext context, RoleManager<RoleEntity> roleManager) : base(context, roleManager: roleManager)
        {
        }

        // GET: api/Camera
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Get()
        {
            try
            {
                var res = await _context.Roles
                    .Select(x => new RoleModel(x))
                    .ToListAsync();

                rspns.Succeed(res);
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }

        // GET: api/Camera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> Get(int id)
        {
            var roleEntity = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (roleEntity == null)
            {
                rspns.Failed("This camera does not exist!");
                return rspns;
            }

            RoleModel cam = new RoleModel(roleEntity);
            rspns.Succeed(cam);

            return rspns;
        }

        // PUT: api/Camera/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Put(int id, RoleModel info)
        {
            RoleEntity role = info.ToEntity(new RoleEntity());
            role.Id = id;

            try
            {
                await _roleManager.UpdateAsync(role);
                rspns.Succeed();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RoleEntityExists(id))
                {
                    rspns.Failed("This role does not exist!");
                }
                else
                {
                    rspns.Failed(ex.Message);
                }
            }

            return rspns;
        }

        // POST: api/Camera
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Post(RoleModel roleDTO)
        {
            RoleEntity role = roleDTO.ToEntity(new RoleEntity());

            try
            {
                await _roleManager.CreateAsync(role);
                rspns.Succeed();
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }

        // DELETE: api/Camera/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            var roleEntity = await _context.Roles.FindAsync(id);
            if (roleEntity == null)
            {
                rspns.Failed("This role does not exist");
                return rspns;
            }

            await _roleManager.DeleteAsync(roleEntity);
            rspns.Succeed();

            return rspns;
        }

        private bool RoleEntityExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}

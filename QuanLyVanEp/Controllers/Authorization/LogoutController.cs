using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.DTO;
using BaseApiWithIdentity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : BaseController
    {
        private SignInManager<UserEntity> _signInManager;

        public LogoutController(AppDbContext context, SignInManager<UserEntity> signInManager) : base (context)
        {
            this._signInManager = signInManager;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                rspns.Succeed();
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }
    }
}

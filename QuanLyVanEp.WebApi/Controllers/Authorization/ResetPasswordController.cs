using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : BaseController
    {
        private SignInManager<UserEntity> _signInManager;

        public ResetPasswordController(AppDbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : base(context, userManager)
        {
            this._signInManager = signInManager;
        }

        // PUT: api/UserPassword/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost("{userId}")]
        public async Task<ResponseModel> ResetPassword(int userId, ResetPwdModel info)
        {
            UserEntity user = _context.Users.Find(userId);

            if (user == null)
            {
                rspns.Failed("User does not exist!");
                return rspns;
            }

            try
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult rslt = await _userManager.ResetPasswordAsync(user, token, info.NewPassword);
                if (rslt.Succeeded)
                {
                    rspns.Succeed();
                    await _signInManager.SignOutAsync();
                }
                else
                {
                    rspns.Failed(rslt.Errors.First().Description);
                }
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }
    }

    public class ResetPwdModel
    {
        public string NewPassword { get; set; }
    }
}

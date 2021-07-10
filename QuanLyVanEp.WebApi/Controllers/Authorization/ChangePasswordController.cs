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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : BaseController
    {

        public ChangePasswordController(AppDbContext context, UserManager<UserEntity> userManager)
        {
        }


        // PUT: api/UserPassword/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ResponseModel> ChangePassword(ChangePasswordModel info)
        {
            UserEntity user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                rspns.Failed("User does not exist!");
                return rspns;
            }

            try
            {
                IdentityResult rslt = await _userManager.ChangePasswordAsync(user, info.Password, info.NewPassword);
                if (rslt.Succeeded)
                {
                    rspns.Succeed();
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

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Password is requried!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "New password is requried!")]
        public string NewPassword { get; set; }
    }
}

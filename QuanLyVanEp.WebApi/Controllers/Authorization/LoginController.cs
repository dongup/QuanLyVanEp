using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DTO;
using BaseApiWithIdentity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly RoleManager<RoleEntity> roleManager;
        private readonly IConfiguration _configuration;

        public LoginController(AppDbContext context,UserManager<UserEntity> _userManager, RoleManager<RoleEntity> roleManager, IConfiguration configuration) : base(context, _userManager)
        {
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Login([FromBody] LoginModel model)
        {
            var user = _context.Users
                .Include(x => x.UserRoles) 
                .ThenInclude(x => x.Role)
                .FirstOrDefault(x => x.UserName == model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(15),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var rslt = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = new BaseUserDTO(user)
                };
                rspns.Succeed(rslt);

            }
            else
            {
                rspns.Failed("Invalid username or password!");
            }
            return rspns;
        }
    }
}

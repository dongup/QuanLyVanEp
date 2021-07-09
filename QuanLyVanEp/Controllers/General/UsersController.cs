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

namespace BaseApiWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        IWebHostEnvironment _webEnv;

        public UsersController(AppDbContext context, UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IWebHostEnvironment hostingEnvironment) : base(context, userManager, roleManager: roleManager)
        {
            _webEnv = hostingEnvironment;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> GetUsers(string keyword)
        {
            try
            {
                var res = await _context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => string.IsNullOrEmpty(keyword)
                            || x.NormalizedUserName.Contains(keyword))
                .Select(x => new BaseUserDTO(x))
                .ToListAsync();

                rspns.Succeed(res);
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
                rspns.Result = ex.StackTrace;
            }

            return rspns;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                rspns.Failed("User does not exist!");
                return rspns;
            }

            var userDTO = new BaseUserDTO(userEntity);
            rspns.Succeed(userDTO);

            return rspns;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ResponseModel> PutUserEntity(int id, BaseUserDTO userDTO)
        {
            if (!UserEntityExists(id))
            {
                rspns.Failed("User does not exist!");
                return rspns;
            }

            UserEntity user = _context.Users.Find(id);
            userDTO.CopyTo(user);
            user.Avatar = SaveImage(user.Avatar);

            try
            {
                RoleEntity newRole = await _roleManager.FindByNameAsync(userDTO.RoleName);
                if(newRole == null)
                {
                    throw new Exception("Please select a valid role!");
                }

                string oldRole = user.UserRoles.FirstOrDefault()?.Role?.Name;
                if (oldRole != "" && oldRole != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, oldRole);
                }

                await _userManager.AddToRoleAsync(user, newRole.Name);
                await _userManager.UpdateAsync(user);
                rspns.Succeed();
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> PostUserEntity(AddUserDTO info)
        {
            var user = info.CopyTo(new UserEntity());
            try
            {
                //user.CreatedUserId = CurrentUserId;
                user.Avatar = SaveImage(user.Avatar);

                IdentityResult result = await _userManager.CreateAsync(user, info.Password);
                if (result.Succeeded)
                {
                    RoleEntity role = await _roleManager.FindByNameAsync(info.RoleName);

                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else
                    {
                        throw new Exception("Please select a role!");
                    }

                    rspns.Succeed();
                }
                else
                {
                    rspns.Failed(result.Errors.FirstOrDefault().Description);
                    rspns.Result = result.Errors;
                }
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
            }

            return rspns;
        }

        private string SaveImage(string file)
        {
            if (!file.Contains("base64")) return file;

            string base64 = file.Split(',')[1];
            string fileInfo = file.Split(',')[0];
            string fileExtension = fileInfo.Split(';')[0].Split('/')[1];

            //string user = CurrentUser.UserName;
            string user = "admin";
            string fileName = $"{Guid.NewGuid().ToString()}.{fileExtension}";

            string savePath = Path.Combine(_webEnv.ContentRootPath, "wwwroot", "Upload", "Avatar", user);
            string filePath = Path.Combine(savePath, fileName);

            string relativePath = $"/Upload/Avatar/{user}/{fileName}";

            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            byte[] bytes = Convert.FromBase64String(base64);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }

            return relativePath;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> DeleteUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                rspns.Failed("User does not exist!");
                return rspns;
            }

            await _userManager.DeleteAsync(userEntity);
            rspns.Succeed();
            return rspns;
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

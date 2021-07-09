using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.Utils;
using System;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using BaseApiWithIdentity.DataAccess.DAL.Entities;

namespace BaseApiWithIdentity.Controllers
{
    public class BaseController : ControllerBase
    {
        private UserEntity currentUser { get; set; }

        public DateTime now => DateTime.Now;

        public ResponseModel rspns = new ResponseModel();

        public int CurrentUserId => CurrentUser.Id;
        public UserEntity CurrentUser {
            get {
                var user = HttpContext.Items["User"];
                if (user == null)
                {
                    throw new Exception("Invalid access token!");
                }
                return (UserEntity)user;
            }
        }

        public readonly AppDbContext _context;
        public readonly UserManager<UserEntity> _userManager;
        public readonly RoleManager<RoleEntity> _roleManager;
        public readonly IWebHostEnvironment _webEnvrm;

        public FileHelper _fileHelper;

        public BaseController(AppDbContext context, UserManager<UserEntity> userManager = null, RoleManager<RoleEntity> roleManager = null, IWebHostEnvironment webEnv = null)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _webEnvrm = webEnv;
        }

        public BaseController()
        {

        }
    }
}

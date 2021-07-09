//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using BaseApiWithIdentity.DataAccess.DAL;
//using BaseApiWithIdentity.DataAccess.DAL.Entities;
//using BaseApiWithIdentity.DataAccess.DTO;
//using BaseApiWithIdentity.DataAccess.Models;
//using BaseApiWithIdentity.Utils;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BaseApiWithIdentity.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UploadController : BaseController
//    {
//        public UploadController(AppDbContext context,IWebHostEnvironment webHost) : base(context, webEnv: webHost)
//        {

//        }

//        [HttpPost]
//        public async Task<ResponseModel> Upload()
//        {
//            var files = Request.Form.Files;
//            _fileHelper = new Utils.FileHelper(_webEnvrm, CurrentUser);

//            LogServices.WriteInfo($"User {CurrentUser.UserName} uploaded {files.Count()} captures");
//            List<CaptureEntity> captures = new List<CaptureEntity>();

//            foreach (var formFile in files)
//            {
//                if (formFile.Length > 0)
//                {
//                    var savePath = _fileHelper.GetNewCaptureSavePath();

//                    using (var stream = System.IO.File.Create(savePath.FilePath))
//                    {
//                        await formFile.CopyToAsync(stream);
//                    }

//                    string thumbnail = _fileHelper.CreateThumbnail(savePath.FilePath);

//                    CaptureEntity cpt = new CaptureEntity();
//                    cpt.CaptureUrl = savePath.RelativeFilePath;
//                    cpt.ThumbnailUrl = thumbnail;
//                    cpt.CreatedUserId = CurrentUserId;

//                    captures.Add(cpt);
//                }
//            }
//            _context.Captures.AddRange(captures);
//            await _context.SaveChangesAsync();
//            rspns.Succeed(captures.Select(x => new CaptureDTO(x)));
//            return rspns;
//        }
//    }
//}

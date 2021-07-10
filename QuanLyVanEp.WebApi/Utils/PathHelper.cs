using Microsoft.AspNetCore.Hosting;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using BaseApiWithIdentity.DataAccess.Utils;

namespace BaseApiWithIdentity.Utils
{
    public class FileHelper
    {
        DateTime now => DateTime.Now;
        private readonly IWebHostEnvironment _webEnvrm;
        private UserEntity _currentUser;
        string user => _currentUser?.UserName;
        public FileHelper(IWebHostEnvironment webHost, UserEntity user)
        {
            _webEnvrm = webHost;
            _currentUser = user;
        }

        public SavePath GetNewCaptureSavePath()
        {
            string fileName = $"CAPTURE_{now.Millisecond}_{now.ToString("HHmmss_ddMMyyyy")}.jpg";

            string relativeSavePath = Path.Combine("Capture", user, now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
            string savePath = Path.Combine(_webEnvrm.ContentRootPath, "wwwroot", relativeSavePath);
            if (!Directory.Exists(savePath)) { Directory.CreateDirectory(savePath); }

            string filePath = Path.Combine(savePath, fileName);
            string relativePath = Path.Combine(relativeSavePath, fileName);

            return new SavePath() {
                FilePath = filePath,
                RelativeFilePath = relativePath,
                FileName = fileName,
                SaveDirectory = savePath
            };
        }

        public string CreateThumbnail(string filePath)
        {
            Image image = Image.FromFile(filePath);
            int thumbnailWidth = image.Width * 10 / 100;
            int thumbnailHeight = image.Height * 10 / 100;

            Image thumb = image.GetThumbnailImage(thumbnailWidth, thumbnailHeight, () => false, IntPtr.Zero);

            string thumbFileName = Path.GetFileNameWithoutExtension(filePath) + "_thumb.jpg";
            string thumbFilePath = Path.Combine(Path.GetDirectoryName(filePath), thumbFileName);

            thumb.Save(thumbFilePath);
            image.Dispose();
            thumb.Dispose();
            return thumbFilePath.RelativePath();
        }
    }

    public class SavePath
    {
        public string FilePath { get; set; }
        public string RelativeFilePath { get; set; }
        public string FileName { get; set; }
        public string SaveDirectory { get; set; }
    }
}

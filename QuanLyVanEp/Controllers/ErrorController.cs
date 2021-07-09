using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.WebApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApiWithIdentity.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class ErrorController : BaseController
    {
        private IConfiguration config;

        public ErrorController(IConfiguration configuration, AppDbContext context) : base(context)
        {
            config = configuration;
        }

        [AcceptVerbs("POST", "GET", "PUT", "DELETE")]
        [Route("handler/{code}")]
        public async Task<ResponseModel> Index(int? code = null)
        {
            if (code != 500) return rspns;

            try
            {
                var err = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                Exception ex = err.Error;

                bool isShowException = config.GetValue<bool>("ShowStackTrace");

                rspns.Failed(ex.Message);

                rspns.Exception = new MyException
                {
                    Message = ex.Message,
                    InnerMessage = ex.InnerException?.Message,
                };

                if (isShowException)
                {
                    rspns.Exception.StackTrace = ex.StackTrace;
                }

                rspns.StatusCode = HttpContext.Response.StatusCode;

                LogModel log = new LogModel(HttpContext);
                log.Level = DataAccess.DAL.Entities.LogEntity.LogLevel.Error;
                log.ExceptionMessage = ex.InnerException == null ? ex.Message : ex.InnerException?.Message;
                log.LogContent = log.ExceptionMessage;
                log.ExceptionDetail = ex.StackTrace;

                log.RequestUrl = err.Path;

                log.RequestBody = await GetReqBody();
                log.Token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                _context.DetachAllEntities();
                _context.Logs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                rspns.Failed(ex.Message);
                rspns.Exception.StackTrace = ex.StackTrace;
            }
            return rspns;
        }

        private async Task<string> GetReqBody()
        {
            string tmp;
            try
            {
                HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't rewind body stream. " + ex.Message);
            }
            using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
            {
                tmp = await reader.ReadToEndAsync();
            }
            return tmp;
        }
    }
}

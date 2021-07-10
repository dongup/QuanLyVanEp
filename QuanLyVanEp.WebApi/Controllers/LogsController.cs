using BaseApiWithIdentity.Controllers;
using BaseApiWithIdentity.DataAccess;
using BaseApiWithIdentity.DataAccess.DAL;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.Models;
using BaseApiWithIdentity.DataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static BaseApiWithIdentity.DataAccess.DAL.Entities.LogEntity;

namespace BaseApiWithIdentity.WebApi.Controllers.Logs
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseController
    {
        public LogsController(AppDbContext context) : base(context)
        {

        }

        [HttpGet()]
        public ResponseModel Get(string keyWord = "", int level = 0
            , string fromDate = "", string toDate = ""
            , int pageItem = 10, int pageIndex = 0)
        {
            var dFromDate = fromDate.ToDateTime().Date;
            var dToDate = toDate.ToDateTime().Date;

            var result = _context.Logs
                .Where(x => (x.Level == (LogLevel)level || level == 0)
                            && (string.IsNullOrWhiteSpace(keyWord) 
                                || x.RequestIpAddress.Contains(keyWord)
                                || x.RequestUrl.Contains(keyWord))
                            && (string.IsNullOrEmpty(fromDate) || x.CreatedDate.Date >= dFromDate)
                            && (string.IsNullOrEmpty(toDate) || x.CreatedDate.Date <= dToDate))
                .OrderByDescending(x => x.CreatedDate)
                .AsNoTracking();

            return rspns.Succeed(new PaginationResponse<LogEntity>(result, pageItem, pageIndex));
        }
    }
}

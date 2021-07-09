using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiyoshaEPE.DataAccess.Models.General;

namespace QuanLyVanEp.DataAccess.DAL.Entities.Tables
{
    public class ProductResponse : BaseResponse
    {
        public ProductResponse() : base()
        {

        }



        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string Desciption { get; set; }
    }
}

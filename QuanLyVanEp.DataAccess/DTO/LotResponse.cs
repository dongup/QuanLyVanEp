using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiyoshaEPE.DataAccess.Models.General;

namespace QuanLyVanEp.DataAccess.DAL.Entities.Tables
{
    public class LotResponse : BaseResponse
    {
        public LotResponse()
        {

        }

        public LotResponse(LotEntity entity) : base(entity)
        {
            if (entity == null) return;
            ModelUtils.CopyProperty(entity, this);
        }

        public string LotNo { get; set; }

        public int SLDatHang { get; set; }

        public int SLXuatKho { get; set; }

        public int SLSX { get; set; }

        public int ProductId { get; set; }

        public virtual ProductResponse Product { get; set; }

        public ICollection<OutputReponse> Outputs { get; set; } = new HashSet<OutputReponse>();
    }
}

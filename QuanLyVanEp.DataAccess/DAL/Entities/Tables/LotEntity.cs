using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVanEp.DataAccess.DAL.Entities.Tables
{
    public class LotEntity : BaseEntity
    {
        public LotEntity()
        {

        }

        public string LotNo { get; set; }

        public int SLDatHang { get; set; }

        public int SLXuatKho { get; set; }

        public int SLSX { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductResponse Product { get; set; }

        public ICollection<OutputEntity> Outputs { get; set; } = new HashSet<OutputEntity>();
    }
}

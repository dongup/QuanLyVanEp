using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;
using QuanLyVanEp.DataAccess.Entities;
using static QuanLyVanEp.DataAccess.Entities.MaterialEntity;

namespace QuanLyVanEp.DataAccess.Entities
{
    /// <summary>
    /// Lịch sử xuất hàng chung của tất cả nguyên vật liệu
    /// </summary>
    public class OutputEntity : BaseOutputEntity
    {
        public OutputEntity()
        {

        }

        public OutputEntity(BaseOutputEntity baseOutput)
        {
            this.AfterNumber = baseOutput.AfterNumber;
            this.OriginNumber = baseOutput.OriginNumber;
            this.OutputNumber = baseOutput.OutputNumber;
            this.MaterialId = baseOutput.MaterialId;
            this.OutputDate = baseOutput.OutputDate;
            this.Purpose = baseOutput.Purpose;
            this.CreatedUserId = baseOutput.CreatedUserId;
            this.InputId = baseOutput.InputId;
        }
        
        public int LotId { get; set; }

        /// <summary>
        /// Thông tin lô hàng nhập
        /// </summary>
        [ForeignKey(nameof(InputId))]
        public InputEntity Input { get; set; }

        /// <summary>
        /// Lịch sử sử dụng của lô hàng xuất
        /// </summary>
        public LotResponse Lots { get; set; }
    }
}

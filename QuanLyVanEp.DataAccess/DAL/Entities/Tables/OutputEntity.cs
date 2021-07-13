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

        /// <summary>
        /// Mã nguyên liệu xuất
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Số lượng lấy
        /// </summary>

        public int OutputNumber { get; set; }

        public DateTime OutputDate { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }
    }
}

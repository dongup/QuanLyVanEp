using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.Entities;
using QuanLyVanEp.DataAccess.Utils;
using static QuanLyVanEp.DataAccess.Entities.MaterialEntity;

namespace QuanLyVanEp.DataAccess.Entities
{
    /// <summary>
    /// Class dùng chung cho tất cả các hàng nguyên vật liệu nhập vào
    /// </summary>
    public class BaseInputEntity : BaseEntity
    {
        private string lotno;
        private string invoiceNumber;
        private string supplier;
        private float inputNumber;
        private float stockNumber;

        public BaseInputEntity() : base()
        {
            //InputDate = DateTime.Now;
        }

        /// <summary>
        /// Số lần nhập trong tháng
        /// </summary>
        [StringLength(30)]
        public string No { get; set; }

        /// <summary>
        /// Mã nguyên liệu nhập
        /// </summary>
        public int? MaterialId { get; set; }

        [Required]
        [StringLength(30)]
        public string Lotno { get => lotno; set => lotno = value.ToUpper(); }

        /// <summary>
        /// Số lượng nhập
        /// </summary>
        public float InputNumber
        {
            get => inputNumber; set
            {
                inputNumber = MathF.Round(value, Constances.SMALL_ROUND_NUMBER);
            }
        }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        public float StockNumber
        {
            get => stockNumber; set
            {
                stockNumber = MathF.Round(value, Constances.SMALL_ROUND_NUMBER);
            }
        }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        [StringLength(30)]
        public string InvoiceNumber { get => invoiceNumber; set => invoiceNumber = value.ToUpper(); }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        [StringLength(100)]
        public string Supplier { get => supplier; set => supplier = value.ToUpper(); }

        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Ngày nhập hàng, có thê khác với ngày tạo
        /// </summary>
        public DateTime InputDate { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public MaterialEntity Material { get; set; }
    }
}

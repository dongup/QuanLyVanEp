using BaseApiWithIdentity.DataAccess.DAL.Entities;
using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class InputEntity : BaseEntity
    {
        private string lotno = "";
        private string invoiceNumber = "";
        private string supplier = "";

        public InputEntity() : base()
        {
        }

        /// <summary>
        /// Mã nguyên liệu nhập
        /// </summary>
        public int? ProductId { get; set; }

        [Required]
        [StringLength(30)]
        public string Lotno { get => lotno?.ToUpper()?.Trim(); set => lotno = (value ?? "").Trim(); }

        /// <summary>
        /// Số lượng nhập
        /// </summary>
        public int InputNumber { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        [StringLength(30)]
        public string InvoiceNumber { get => invoiceNumber.ToUpper(); set => invoiceNumber = value ?? ""; }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        [StringLength(100)]
        public string Supplier { get => supplier.ToUpper(); set => supplier = value ?? ""; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }
    }
}

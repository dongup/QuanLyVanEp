using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class InputResponse : BaseResponse
    {
        private string lotno = "";
        private string invoiceNumber = "";
        private string supplier = "";
        private string no;
        private DateTime? bindExpireDate = DateTime.Now;
        private DateTime expireDate;

        public InputResponse() : base()
        {
        }

        public InputResponse(BaseInputEntity entity) : base(entity)
        {
            if (entity == null) return;
            ModelUtils.CopyProperty(entity, this);

            Material = new MaterialResponse(entity.Material);
        }

        public bool ShowDetail { get; set; }

        /// <summary>
        /// Số lần nhập trong tháng
        /// </summary>
        [StringLength(30)]
        public string No { get => no?.ToUpper(); set => no = value ?? ""; }

        /// <summary>
        /// Mã nguyên liệu nhập
        /// </summary>
        public int? MaterialId { get; set; }

        [Required]
        [StringLength(30)]
        public string Lotno { get => lotno?.ToUpper()?.Trim(); set => lotno = (value ?? "").Trim(); }

        /// <summary>
        /// Số lượng nhập
        /// </summary>
        public float InputNumber { get; set; }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        public float StockNumber { get; set; }

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

 
        /// Ngày nhập hàng, có thê khác với ngày tạo
        /// </summary>
        public DateTime InputDate { get; set; } = DateTime.Now;

        public MaterialResponse Material { get; set; } = new MaterialResponse();

        public List<OutputReponse> Outputs { get; set; } = new List<OutputReponse>();
    }
}

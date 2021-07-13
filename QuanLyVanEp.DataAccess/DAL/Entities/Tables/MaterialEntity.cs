using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaiyoshaEPE.DataAccess.Models.General;

namespace QuanLyVanEp.DataAccess.Entities
{

    /// <summary>
    /// Chưa thông tin của các nguyên vật liệu
    /// </summary>
    public class MaterialEntity : BaseEntity
    {
        private float stockNumber;
        private float inputNumber;

        public MaterialEntity() : base()
        {
            Type = MaterialType.Paste;
        }

        /// <summary>
        /// Mã vật liệu
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập mã nvl")]
        [StringLength(30)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên vật liệu
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên nvl")]
        public string MaterialName { get; set; }

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
        /// Loại vật liệu
        /// </summary>
        [Required(ErrorMessage = "Vui lòng chọn loại vật liệu")]
        public MaterialType Type { get; set; }

        /// <summary>
        /// Chỉ dành cho sokumen
        /// </summary>
        public float HeSo { get; set; } = 1;

        public int ExpireDateWarning { get; set; }

        /// <summary>
        /// Định nghĩa loại vật liệu
        /// </summary>
        public enum MaterialType
        {
            [Description("Kiban")]
            Kiban = 1,
            [Description("Mực")]
            Paste = 2,
            [Description("Mực Sokumen")]
            Sokumen = 3,
            [Description("Hóa chất")]
            Chemial = 4,
            [Description("Tapping")]
            Tapping = 5,
            [Description("Đóng gói")]
            Wrapping = 6
        }

        /// <summary>
        /// Lịch sử nhập hàng của nguyên vật liệu này
        /// </summary>
        public List<InputEntity> Inputs { get; set; }

        /// <summary>
        /// Lịch sử xuất hàng của nguyên vật liệu này
        /// </summary>
        public List<OutputEntity> Outputs { get; set; }
    }
}

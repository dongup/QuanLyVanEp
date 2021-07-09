using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.Entities;
using QuanLyVanEp.DataAccess.Utils;
using static QuanLyVanEp.DataAccess.Entities.MaterialEntity;

namespace QuanLyVanEp.DataAccess.Entities
{
    /// <summary>
    /// Class dùng chung cho tất cả các hàng nguyên vật liệu xuất ra
    /// </summary>
    public class BaseOutputEntity : BaseEntity
    {
        private string lotno;
        private float outputNumber;

        public BaseOutputEntity()
        {
        }

        /// <summary>
        /// Mỗi lần lấy ra gắn với môt lot hàng nhập để trừ tồn
        /// Có thê null vì khi lấy có thê không nhập lotno
        /// </summary>
        public int? InputId { get; set; }

        public string Lotno { get => lotno; set => lotno = value; }

        /// <summary>
        /// Mã nguyên liệu xuất
        /// </summary>
        public int? MaterialId { get; set; }

        /// <summary>
        /// Số lượng lấy
        /// </summary>

        public float OutputNumber
        {
            get => outputNumber; set
            {
                outputNumber = MathF.Round(value, Constances.SMALL_ROUND_NUMBER);
            }
        }

        /// <summary>
        /// Số lượng trước khi lấy
        /// </summary>
        [Display(Name = "KL cân")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must greater than 0")]
        public float OriginNumber { get; set; }

        /// <summary>
        /// Số lượng sau khi lấy
        /// </summary>
        [Display(Name = "KL cân")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must greater than 0")]
        public float AfterNumber { get; set; }

        /// <summary>
        /// Mục đích xuất
        /// </summary>
        public OutPurpose Purpose { get; set; }

        public enum OutPurpose
        {
            [Description("In hàng")]
            Print = 1,
            [Description("In Dami")]
            Dami = 2,
            [Description("Trộn")]
            Mix = 3,
            [Description("Chiết mực")]
            Don = 4,
            [Description("Nhận mực chiết")]
            DuocDon = 5,
            [Description("Tách mực")]
            TachMuc = 6,
            [Description("Hủy mực")]
            Discard = 7,
            [Description("Test mực")]
            Test = 8,
        }

        public string WastedNote { get; set; }

        public DateTime OutputDate { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public MaterialEntity Material { get; set; }
    }
}

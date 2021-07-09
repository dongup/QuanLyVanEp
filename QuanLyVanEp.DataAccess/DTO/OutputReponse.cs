using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class OutputReponse : BaseResponse
    {
        private float outputNumber;
        private int? machineId = null;

        public OutputReponse() : base()
        {
        }

        public OutputReponse(BaseOutputEntity entity) : base(entity)
        {
            if (entity == null) return;
            ModelUtils.CopyProperty(entity, this);
            Material = new MaterialResponse(entity.Material);
        }


        /// <summary>
        /// Mỗi lần lấy ra gắn với môt lot hàng nhập để trừ tồn
        /// Có thê null vì khi lấy có thê không nhập lotno
        /// </summary>
        public int? InputId { get; set; }

        public string Lotno { get; set; }

        /// <summary>
        /// Mã nguyên liệu xuất
        /// </summary>
        public int? MaterialId { get; set; }

        /// <summary>
        /// Số lượng lấy
        /// </summary>

        public float OutputNumber
        {
            get => outputNumber;
            set
            {
                outputNumber = value;
            }
        }

        /// <summary>
        /// Số lượng trước khi lấy
        /// </summary>
        [Display(Name = "KL cân")]
        public float OriginNumber { get; set; }

        /// <summary>
        /// Số lượng sau khi lấy
        /// </summary>
        [Display(Name = "KL cân")]
        public float AfterNumber { get; set; }
     
        public DateTime OutputDate { get; set; }

        public int? MachineId
        {
            get => machineId; set
            {
                machineId = value;
            }
        }

        public MaterialResponse Material { get; set; }
    }
}

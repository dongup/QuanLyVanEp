using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class MaterialResponse : BaseResponse
    {
        public MaterialResponse()
        {

        }

        public MaterialResponse(MaterialEntity entity) : base(entity)
        {
            if (entity == null) return;
            ModelUtils.CopyProperty(entity, this);
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
        public float InputNumber { get; set; }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        public float StockNumber { get; set; }


        public int MaterialGroup { get; set; }
    }
}

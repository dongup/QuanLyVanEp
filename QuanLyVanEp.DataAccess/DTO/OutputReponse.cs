using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class OutputReponse : BaseResponse
    {
        private int outputNumber;

        public OutputReponse() : base()
        {
        }

        public OutputReponse(OutputEntity entity) : base(entity)
        {
            if (entity == null) return;
            ModelUtils.CopyProperty(entity, this);
        }

        /// <summary>
        /// Mã nguyên liệu xuất
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Số lượng lấy
        /// </summary>

        public int OutputNumber
        {
            get => outputNumber;
            set
            {
                outputNumber = value;
            }
        }
    }
}

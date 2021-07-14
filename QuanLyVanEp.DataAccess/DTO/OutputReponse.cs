using BaseApiWithIdentity.DataAccess.Utils;
using QuanLyVanEp.DataAccess.Entities;

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

        public string OutputCode { get; set; }

        public int OutputNumber { get => outputNumber; set => outputNumber = value; }

        public int? ProductId { get; set; }

        public double OutputPrice { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;

namespace QuanLyVanEp.DataAccess.Entities
{

    /// <summary>
    /// Lịch sử sử dụng chung cho tất cả nguyên vật liệu
    /// </summary>
    public class BaseUseHistoryEntity : BaseEntity
    {
        public BaseUseHistoryEntity()
        {

        }

        /// <summary>
        /// Lô hàng được sử dụng
        /// </summary>
        public int? LotId { get; set; }

        public int ProcessId { get; set; }

        public string ShoteikoLotno { get; set; }

        /// <summary>
        /// Mỗi lần sử dụng gắn với một lần xuất hàng
        /// </summary>
        public int? OutputId { get; set; }

        /// <summary>
        /// Số lượng tấm kiban nhận được
        /// </summary>
        public int InputNum { get; set; }

        /// <summary>
        /// Số lượng tấm kiban sau khi kết thúc công đoạn
        /// </summary>
        public int ProductionNum { get; set; }

        /// <summary>
        /// Số lượng tấm kiban chệnh so với số Kiban từ quy trình trước
        /// </summary>
        public int DiffNum { get; set; }

        /// <summary>
        /// Số lượng sử dụng
        /// </summary>
        public float UsedNumber { get; set; }

        /// <summary>
        /// Mục đích sử dụng
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Nội dung phế phẩm
        /// </summary>
        public string WastedNote { get; set; }

        /// <summary>
        /// Mỗi lần sử dụng cần lưu lại số máy
        /// </summary>
        public int? MachineId { get; set; }

        [ForeignKey(nameof(LotId))] 
        public LotResponse Lot { get; set; }
    }
}

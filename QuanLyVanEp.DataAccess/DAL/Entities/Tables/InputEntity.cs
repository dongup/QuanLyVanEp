using System;
using System.Collections.Generic;
using QuanLyVanEp.DataAccess.Entities;

namespace QuanLyVanEp.DataAccess.Entities
{
    /// <summary>
    /// Bảng lưu thông tin nhập chung cho tất cả vật liệu
    /// </summary>
    public class InputEntity : BaseInputEntity
    {
        public InputEntity(BaseInputEntity baseInput)
        {
            CreatedDate = DateTime.Now;
            MaterialId = baseInput.MaterialId;
            this.StockNumber = baseInput.StockNumber;
            this.InputNumber = baseInput.InputNumber;
            this.InputDate = baseInput.InputDate;
            this.InvoiceNumber = baseInput.InvoiceNumber;
            this.Supplier = baseInput.Supplier;
            this.Lotno = baseInput.Lotno;
            this.No = baseInput.No;
            this.ExpireDate = baseInput.ExpireDate;
        }

        public InputEntity()
        {
            Outputs = new HashSet<OutputEntity>();
        }
        
        /// <summary>
        /// Lịch sử xuất của lot nguyên vật liệu này
        /// </summary>
        public ICollection<OutputEntity> Outputs { get; set; }
    }
}

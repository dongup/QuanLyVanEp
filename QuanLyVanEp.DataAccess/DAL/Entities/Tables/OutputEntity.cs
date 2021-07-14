using System;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;

namespace QuanLyVanEp.DataAccess.Entities
{
    public class OutputEntity : BaseOutputEntity
    {
        public OutputEntity()
        {
        }

        public string OutputCode { get; set; }

        public int OutputNumber { get; set; }

        public double OutputPrice { get; set; }

        public DateTime OutputDate { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }

        public int? ProductId { get; set; }
    }
}

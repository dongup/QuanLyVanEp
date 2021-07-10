using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVanEp.DataAccess.Entities.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }
    }
}

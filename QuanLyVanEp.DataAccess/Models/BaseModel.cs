using System;

namespace QuanLyVanEp.DataAccess.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

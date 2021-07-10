using QuanLyVanEp.DataAccess.Entities.Base;

namespace QuanLyVanEp.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public Product() : base()
        {
        }

        public string Name { get; set; }
    }
}

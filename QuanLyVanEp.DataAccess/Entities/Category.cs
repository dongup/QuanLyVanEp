using QuanLyVanEp.DataAccess.Entities.Base;
using System.Collections.Generic;

namespace QuanLyVanEp.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public Category() : base()
        {
        }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

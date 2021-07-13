using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;
using System.Collections.Generic;

namespace QuanLyVanEp.DataAccess.DAL
{
    public class ProductCategoryEntity : BaseEntity
    {
        public ProductCategoryEntity() : base()
        {
        }

        public string Name { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
    }

    public class ProductCatergoryRequest
    {
        public ProductCatergoryRequest()
        {

        }

        public string Name { get; set; }
    }
}

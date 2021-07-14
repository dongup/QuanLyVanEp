using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVanEp.DataAccess.DAL.Entities.Tables
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity() : base()
        {
        }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string Desciption { get; set; }

        public int CategoryId { get; set; }

        public int StockNumber { get; set; }

        public int SoldNumber { get; set; }

        public string CategoryName { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ProductCategoryEntity ProductCategory { get; set; }
    }

    public class ProductRequest
    {
        public ProductRequest()
        {
        }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string Desciption { get; set; }

        public int CategoryId { get; set; }
    }
}

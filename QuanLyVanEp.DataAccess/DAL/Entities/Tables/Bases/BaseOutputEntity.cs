using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseApiWithIdentity.DataAccess.DAL.Entities;
using QuanLyVanEp.DataAccess.DAL.Entities.Tables;
using QuanLyVanEp.DataAccess.Entities;
using QuanLyVanEp.DataAccess.Utils;
using static QuanLyVanEp.DataAccess.Entities.MaterialEntity;

namespace QuanLyVanEp.DataAccess.Entities
{
    /// <summary>
    /// Class dùng chung cho tất cả các hàng nguyên vật liệu xuất ra
    /// </summary>
    public class BaseOutputEntity : BaseEntity
    {
        private string lotno;
        private float outputNumber;

        public BaseOutputEntity()
        {
        }
    }
}

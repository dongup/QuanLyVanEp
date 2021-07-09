using BaseApiWithIdentity.DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaiyoshaEPE.DataAccess.Models.General
{
    public class BaseResponse
    {
        private int createdUserId;

        public BaseResponse()
        {
            CreatedDate = DateTime.Now;
        }

        public BaseResponse(BaseEntity entity)
        {
            if (entity == null) return;
            Id = entity.Id;
            CreatedDate = entity.CreatedDate;
            CreatedUserId = entity.CreatedUserId;
        }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUserId { get => createdUserId; set => createdUserId = value; }

        protected DateTime? DeletedDate { get; set; }
    }
}

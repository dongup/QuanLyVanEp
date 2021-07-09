using BaseApiWithIdentity.DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApiWithIdentity.DataAccess.DTO
{
    public class BaseDTO
    {
        public BaseDTO()
        {

        }

        public BaseDTO(BaseEntity baseEntity)
        {
            Id = baseEntity.Id;
            CreatedDate = baseEntity.CreatedDate;
            CreatedUserId = baseEntity.CreatedUserId;
            UpdatedDate = baseEntity.UpdatedDate;
            UpdatedUserId = baseEntity.UpdatedUserId;
        }

        public void CopyTo(BaseEntity entity)
        {

        }

        protected int Id { get; set; } = 0;

        protected DateTime CreatedDate { get; set; }
        protected int CreatedUserId { get; set; }

        protected DateTime UpdatedDate { get; set; }
        protected int UpdatedUserId { get; set; }
    }
}

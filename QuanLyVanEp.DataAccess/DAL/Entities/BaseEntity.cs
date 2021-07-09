using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseApiWithIdentity.DataAccess.DAL.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        public BaseEntity(UserManager<UserEntity> userManager)
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }

        public DateTime UpdatedDate { get; set; }
        public int UpdatedUserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}

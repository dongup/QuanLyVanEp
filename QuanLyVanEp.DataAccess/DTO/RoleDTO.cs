using System.ComponentModel.DataAnnotations;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;

namespace BaseApiWithIdentity.DataAccess.DTO
{
    public class RoleModel : BaseDTO
    {
        [Required(ErrorMessage = "Please provide role's name!")]
        public string Name { get; set; }

        protected string ConcurentStamp { get; set; }

        public RoleModel()
        {
        }

        public RoleModel(RoleEntity role)
        {
            Id = role.Id;
            Name = role.Name;
            ConcurentStamp = role.ConcurrencyStamp;
        }

        public RoleEntity ToEntity(RoleEntity role)
        {
            role.Name = Name;
            return role;
        }
    }

}

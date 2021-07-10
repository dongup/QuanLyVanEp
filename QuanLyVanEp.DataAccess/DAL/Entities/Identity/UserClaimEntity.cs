using Microsoft.AspNetCore.Identity;

namespace BaseApiWithIdentity.DataAccess.DAL.Entities.Identity
{
    public class UserClaimEntity : IdentityUserClaim<int>
    {
        public bool IsDeleted { get; set; }
    }
}

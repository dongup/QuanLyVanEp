using Microsoft.AspNetCore.Identity;

namespace BaseApiWithIdentity.DataAccess.DAL.Entities.Identity
{
    public class UserTokenEntity : IdentityUserToken<int>
    {
        public bool IsDeleted { get; set; }
    }
}

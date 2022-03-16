using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace wakeApi.Identity
{
    public class Role : IdentityRole<int>
    {
        public virtual User? User { get; set; }
        public virtual List<UserRole>? UserRoles { get; set; }

    }
}

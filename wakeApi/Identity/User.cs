using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace wakeApi.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(256)")]
        public string FullName { get; set; } = string.Empty;
        public List<UserRole>? UserRoles { get; set; }
    }
}

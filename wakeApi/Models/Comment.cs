using wakeApi.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace wakeApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string CommentText { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int PostId { get; set; }
        public virtual PostVideo? PostVideo { get; set; }
    }
}

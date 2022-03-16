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
        public User? User { get; set; }
        public int PostId { get; set; }
        public PostVideo? PostVideo { get; set; }
    }
}

using wakeApi.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace wakeApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string CommentText { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int PostId { get; set; }
        public bool Flag { get; set; }
    }
}

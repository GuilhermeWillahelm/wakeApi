using System.ComponentModel.DataAnnotations.Schema;

namespace wakeApi.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string CommentText { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual UserDto? User { get; set; }
        public int PostId { get; set; }
        public virtual PostVideoDto? PostVideo { get; set; }
    }
}

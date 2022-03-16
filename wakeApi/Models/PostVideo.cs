using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wakeApi.Identity;

namespace wakeApi.Models
{
    public class PostVideo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Posted { get; set; }
        public string Video { get; set; } = string.Empty;
        [NotMapped]
        public FormFile? FormFile { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}

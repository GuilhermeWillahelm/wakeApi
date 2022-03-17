using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wakeApi.Dtos
{
    public class ChannelDto
    {
        public int Id { get; set; }
        public string ChannelName { get; set; } = string.Empty;
        public string ChannelDescription { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime CreatedChanel { get; set; }
        public string ImageBanner { get; set; } = string.Empty;
        [NotMapped]
        public FormFile? File { get; set; }
        public int UserId { get; set; }
        public virtual UserDto? User { get; set; }
        public int FollwerId { get; set; }
        public virtual FollowerDto? Follower { get; set; }
    }
}

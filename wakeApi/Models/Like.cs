using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wakeApi.Dtos;
using wakeApi.Identity;

namespace wakeApi.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int CountLike { get; set; }
        public int CountDislike { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int PostId { get; set; }
        public bool Flag { get; set; }
        public List<PostVideo>? PostVideos { get; set; }

    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using wakeApi.Models;

namespace wakeApi.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string CommentText { get; set; } = string.Empty;
        public int UserId { get; set; }
        public UserDto? UserDto { get; set; }
        public int ChannelId { get; set; }
        public ChannelDto? ChannelDto { get; set; }
        public int PostId { get; set; }
        public bool Flag { get; set; }
        public List<PostVideoDto>? PostVideoDtos { get; set; }
    }
}

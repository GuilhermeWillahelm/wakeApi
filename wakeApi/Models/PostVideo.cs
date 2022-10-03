﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wakeApi.Identity;

namespace wakeApi.Models
{
    public class PostVideo
    {
        internal object PostVideos;

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Posted { get; set; }
        public string VideoFile { get; set; } = string.Empty;
        public string ThumbImage { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int ChannelId { get; set; }
        public virtual Channel? Channel { get; set; }
        public int EvaluationId { get; set; }
        public virtual Evaluation? Evaluation { get; set; }
        public int CommentId { get; set; }
        public virtual Comment? Comment { get; set; }
    }
}
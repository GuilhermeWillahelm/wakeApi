﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wakeApi.Dtos
{
    public class PostVideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Posted { get; set; }
        public string Video { get; set; } = string.Empty;
        [NotMapped]
        public FormFile? FileVideo { get; set; }
        public string ThumbImage { get; set; } = string.Empty;
        [NotMapped]
        public FormFile? FileImage { get; set; }
        public int UserId { get; set; }
        public virtual UserDto? UserDto { get; set; }
    }
}

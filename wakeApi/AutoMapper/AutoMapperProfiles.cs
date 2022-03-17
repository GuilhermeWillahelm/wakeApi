﻿using wakeApi.Dtos;
using wakeApi.Models;
using wakeApi.Identity;
using AutoMapper;

namespace wakeApi.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {

       public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<PostVideo, PostVideoDto>().ReverseMap();
            CreateMap<Channel, ChannelDto>().ReverseMap();
            CreateMap<Follower, FollowerDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();
        }
    }
}

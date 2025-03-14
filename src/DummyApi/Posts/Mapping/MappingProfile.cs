using AutoMapper;
using DummyApi.Posts.Models;

namespace DummyApi.Posts.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>().ReverseMap();
    }
}
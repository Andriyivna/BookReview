using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorReturnDto>().ReverseMap();
            CreateMap<Author, AuthorAddDto>().ReverseMap();
        }
    }
}

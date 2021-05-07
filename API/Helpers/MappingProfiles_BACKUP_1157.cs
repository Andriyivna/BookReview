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
            CreateMap<VirtualLibrary, VirtualLibraryDto>();
            CreateMap<VirtualLibraryBook, VirtualBookReturnDto>()
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Book.Author.FirstName + " " + src.Book.Author.SecondName))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Book.Genre.Name))
                .ForMember(
                    dest => dest.CoverImg,
                    opt => opt.MapFrom(src => src.Book.CoverImg))
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(
                    dest => dest.BookId,
                    opt => opt.MapFrom(src => src.Book.Id));
            CreateMap<VirtualBookAddDto, VirtualLibraryBook>();
<<<<<<< HEAD
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Avatar,
                    opt => opt.MapFrom<AvatarUserUrlResolver>());
            CreateMap<Avatar, AvatarDto>()
                .ForMember(
                    dest => dest.Url,
                    opt => opt.MapFrom<AvatarUrlResolver>());
=======
            CreateMap<Book, BetterBook>()
                 .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.SecondName))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre.Name));
           
>>>>>>> sprintIV
        }
    }
}

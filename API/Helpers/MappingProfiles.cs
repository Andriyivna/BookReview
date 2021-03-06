using API.Dtos;
using API.Entities;
using AutoMapper;
using System;

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

            CreateMap<Book, BetterBook>()
                 .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.SecondName))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Book, AddBook>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Avatar,
                    opt => opt.MapFrom<AvatarUserUrlResolver>());
            CreateMap<Avatar, AvatarDto>()
                .ForMember(
                    dest => dest.Url,
                    opt => opt.MapFrom<AvatarUrlResolver>());
            CreateMap<ReviewAddDto, Review>()
                .ForMember(
                    dest => dest.GivenRate,
                    opt => opt.MapFrom(src => Math.Round(src.GivenRate, 1)));
            CreateMap<Review, ReviewReturnDto>();
            CreateMap<Genre, GenreDto>();

            CreateMap<Quote, QuoteDTO>()
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.SecondName)
                    );
        
            CreateMap<Quote, QuoteOfTheDayDto>()
                .ForMember(
                    dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.SecondName}"));
        }
    }
}

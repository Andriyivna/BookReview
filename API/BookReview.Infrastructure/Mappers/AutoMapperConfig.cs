using AutoMapper;
using BookReview.DataBase.DataBaseModels;
using BookReview.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookReview.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Books, BooksDTO>();
            })
            .CreateMapper();
    }
}

using AutoMapper;
using System.Linq;
using BookAPI.Domain.Models;
using BookAPI.Resources;

namespace BookAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Author, AuthorResource>();
            CreateMap<Genre, GenreResource>();
            CreateMap<Publisher, PublisherResource>();
            CreateMap<Book, BookResource>()
                    .ForMember(dest => dest.Genre, src => src.MapFrom(x => x.Genre.Name))
                    .ForMember(dest => dest.Publisher, src => src.MapFrom(x => x.Publisher.Name))
                    .ForMember(dest => dest.City, src => src.MapFrom(x => x.Publisher.City))
                    .ForMember(dest => dest.Authors, src => src.MapFrom(x => x.Authors.Select(y => y.Author)))
                    .ForMember(dest => dest.BooksForSale, src => src.MapFrom(x => x.BooksForSale));
            CreateMap<User, UserResource>()
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.UserRoles.Select(y => y.Role.Name)))
                .ForMember(dest => dest.BooksForSale, src => src.MapFrom(x => x.BooksForSale));
            CreateMap<Role, RoleResource>();
            CreateMap<BookForSale, BookForSaleResource>()
                    .ForMember(dest => dest.Sold, src => src.MapFrom(x => x.BookOrdered.Sold))
                    .ForMember(dest => dest.Firstname, src => src.MapFrom(x => x.BookOrdered.User.Firstname))
                    .ForMember(dest => dest.Lastname, src => src.MapFrom(x => x.BookOrdered.User.Lastname))
                    .ForMember(dest => dest.Login, src => src.MapFrom(x => x.BookOrdered.User.Login))
                    .ForMember(dest => dest.Phone, src => src.MapFrom(x => x.BookOrdered.User.Phone));
            CreateMap<BookOrdered, BookOrderedResource>()
                    .ForMember(dest => dest.Firstname, src => src.MapFrom(x => x.User.Firstname))
                    .ForMember(dest => dest.Lastname, src => src.MapFrom(x => x.User.Lastname))
                    .ForMember(dest => dest.Login, src => src.MapFrom(x => x.User.Login))
                    .ForMember(dest => dest.Phone, src => src.MapFrom(x => x.User.Phone))
                    .ForMember(dest => dest.BookForsale, src => src.MapFrom(x => x.BookForSale));
        }
    }
}
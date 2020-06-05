using AutoMapper;
using System.Linq;
using BookAPI.Domain.Models;
using BookAPI.Resources;

namespace BookAPI.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAuthorResource, Author>();
            CreateMap<SaveGenreResource, Genre>();
            CreateMap<SavePublisherResource, Publisher>();
            CreateMap<SaveBookResource, Book>() 
                    .ForMember(dest => dest.Genre, src => src.MapFrom(x => new SaveGenreResource(){Name = x.Genre}))
                    .ForMember(dest => dest.Publisher, src => src.MapFrom(x => new SavePublisherResource(){ Name = x.Publisher, City = x.City}))
                    .ForMember(dest => dest.Authors, src => src.MapFrom(x => x.Authors.Select(y => new AuthorBook(){ Author = new Author(){LastName = y.LastName, FirstName = y.FirstName, Patronymic = y.Patronymic}})));
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveRoleResource, Role>();
            CreateMap<SaveBookForSaleResource, BookForSale>();
            CreateMap<SaveBookOrderedResource, BookOrdered>();
        }
    }
}
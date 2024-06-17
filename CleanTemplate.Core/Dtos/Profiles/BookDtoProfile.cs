using AutoMapper;
using CleanTemplate.Core.Entities;

namespace CleanTemplate.Core.Dtos.Profiles
{
    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author!.Name));
        }
    }
}

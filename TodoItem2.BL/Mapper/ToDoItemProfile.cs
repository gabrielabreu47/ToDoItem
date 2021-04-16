using AutoMapper;
using TodoItem2.Model.Entities;
using ToDoItem2.BL.Dtos;

namespace ToDoItem2.BL.Mapper
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(destination => destination.FullName, options => options.MapFrom(source => $"{source.Name} - {source.Description}"))
                .ReverseMap();
        }
    }
}

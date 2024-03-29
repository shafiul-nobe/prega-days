using AutoMapper;
using PregaDays.Models.Domain;
using PregaDays.Models.DTO;

namespace PregaDays.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Day,DayDto>().ReverseMap();
            CreateMap<Day, AddDayRequestDto>().ReverseMap();
        }
    }
}

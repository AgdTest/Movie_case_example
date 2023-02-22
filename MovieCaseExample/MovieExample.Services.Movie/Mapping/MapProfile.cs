using AutoMapper;
using MovieExample.Services.Movie.Dtos;

namespace MovieExample.Services.Movie.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MovieExample.Services.Movie.Models.Movie, MovieDto>().ReverseMap();
            CreateMap<MovieExample.Services.Movie.Models.Review, ReviewDto>().ReverseMap();
        }
    }
}

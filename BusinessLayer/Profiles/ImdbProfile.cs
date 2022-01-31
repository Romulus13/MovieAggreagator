using AutoMapper;
using BusinessLayer.Model;
using BusinessLayer.Models;
using DataLayer.Models;
using IMDbApiLib.Models;


namespace MovieAggreagator.Profiles
{
    public class ImdbProfile : Profile
    {
        public ImdbProfile()
        {
            CreateMap<SearchData, ImdbSearchResult>()
                .ForMember(
                    dest => dest.SearchType,
                    opt => opt.MapFrom(src => $"{src.SearchType}")
                )
                .ForMember(
                    dest => dest.ErrorMessage,
                    opt => opt.MapFrom(src => $"{src.ErrorMessage}")
                )
                .ForMember(
                    dest => dest.Expression,
                    opt => opt.MapFrom(src => $"{src.Expression}")
                );

            CreateMap<SearchResult, ImdbSearchResultItem>();

            CreateMap<ImdbStoredResult, ImdbSearchResultItem>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ImdbId ?? String.Empty))
             .ForMember(dest => dest.ResultType, opt => opt.MapFrom(src => src.ResultType ?? String.Empty))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? String.Empty))
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image ?? String.Empty))
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title ?? String.Empty));

            CreateMap<ImdbSearchResultItem, ImdbStoredResult>()
            .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.Id ?? String.Empty))
             .ForMember(dest => dest.ResultType, opt => opt.MapFrom(src => src.ResultType ?? String.Empty))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? String.Empty))
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image ?? String.Empty))
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title ?? String.Empty))
             .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<YoutubeStoredResult, YoutubeSearchResultItem>().ReverseMap();
        }
    }
}

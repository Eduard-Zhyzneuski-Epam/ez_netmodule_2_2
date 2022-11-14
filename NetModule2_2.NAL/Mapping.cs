using AutoMapper;

namespace NetModule2_2.NAL
{
    public static class Mapping
    {
        private static IMapper mapper;

        internal static TDestination Map<TSource, TDestination>(TSource source)
        {
            if (mapper is null)
                Init();
            return mapper.Map<TSource, TDestination>(source);
        }

        private static void Init()
        {
            mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<BAL.Category, NAL.Models.Category>()
                    .ForMember(dest => dest.Link, opt => opt.MapFrom(src => ResourceNavigator.CategoryLink(src.Id)))
                    .ForMember(dest => dest.ParentCategoryLink, opt => opt.MapFrom(src => ResourceNavigator.CategoryLink(src.ParentCategoryId)))
                    .ForMember(dest => dest.CategoryItemsLink, opt => opt.MapFrom(src => ResourceNavigator.ItemsLink(src.Id, 1)));
                config.CreateMap<NAL.Models.NewCategory, BAL.Category>();
                config.CreateMap<NAL.Models.UpdatedCategory, BAL.Category>();
                config.CreateMap<BAL.Item, NAL.Models.Item>()
                    .ForMember(dest => dest.Link, opt => opt.MapFrom(src => ResourceNavigator.ItemLink(src.Id)))
                    .ForMember(dest => dest.CategoryLink, opt => opt.MapFrom(src => ResourceNavigator.CategoryLink(src.CategoryId)));
                config.CreateMap<NAL.Models.NewItem, BAL.Item>();
                config.CreateMap<NAL.Models.UpdatedItem, BAL.Item>();
            }).CreateMapper();
        }
    }
}

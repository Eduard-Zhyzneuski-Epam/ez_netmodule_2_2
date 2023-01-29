using AutoMapper;

namespace NetModule2_2.GAL
{
    internal static class Mapping
    {
        private static IMapper mapper = null;

        private static void Init()
        {
            mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<BAL.Category, GAL.Model.Category>();
                config.CreateMap<GAL.Model.NewCategory, BAL.Category>();
                config.CreateMap<GAL.Model.UpdatedCategory, BAL.Category>();
                config.CreateMap<BAL.Item, GAL.Model.Item>();
                config.CreateMap<GAL.Model.NewItem, BAL.Item>();
                config.CreateMap<GAL.Model.UpdatedCategory, BAL.Item>();
            }).CreateMapper();
        }

        internal static T2 Map<T1, T2>(T1 source)
        {
            if (mapper == null)
            {
                Init();
            }
            return mapper.Map<T1, T2>(source);
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.BAL
{
    internal static class Mapping
    {
        private static IMapper mapper = null;

        private static void Init()
        {
            mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<DAL.Category, BAL.Category>();
                config.CreateMap<DAL.Item, BAL.Item>();
                config.CreateMap<BAL.Category, DAL.Category>();
                config.CreateMap<BAL.Item, DAL.Item>();
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

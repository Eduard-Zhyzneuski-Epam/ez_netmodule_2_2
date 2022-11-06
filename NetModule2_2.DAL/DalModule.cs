using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
        }
    }
}

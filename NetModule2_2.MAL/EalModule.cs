﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.EAL
{
    public class EalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChangedItemEventPublisher>().As<IChangedItemEventPublisher>().InstancePerLifetimeScope();
        }
    }
}

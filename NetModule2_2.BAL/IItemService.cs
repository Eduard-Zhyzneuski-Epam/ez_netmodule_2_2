﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.BAL
{
    public interface IItemService
    {
        Item Get(int id);
        List<Item> List();
        void Add(Item item);
        void Update(Item item);
        void Delete(int id);
    }
}

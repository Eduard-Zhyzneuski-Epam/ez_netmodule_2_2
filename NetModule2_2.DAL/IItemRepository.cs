using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    public interface IItemRepository
    {
        Item Get(string name);
        List<Item> List();
        void Add(Item item);
        void Update(Item item);
        void Delete(string name);
    }
}

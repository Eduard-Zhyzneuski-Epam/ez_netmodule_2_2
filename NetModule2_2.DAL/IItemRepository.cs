using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    public interface IItemRepository
    {
        Item Get(int id);
        List<Item> List(int? categoryId);
        int Add(Item item);
        void Update(Item item);
        void Delete(int id);
        void DeleteCategory(int categoryId);
    }
}

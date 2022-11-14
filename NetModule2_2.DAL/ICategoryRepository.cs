using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    public interface ICategoryRepository
    {
        Category Get(int id);
        List<Category> List();
        int Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}

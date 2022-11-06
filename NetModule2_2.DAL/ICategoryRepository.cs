using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.DAL
{
    public interface ICategoryRepository
    {
        Category Get(string name);
        List<Category> List();
        void Add(Category category);
        void Update(Category category);
        void Delete(string name);
    }
}

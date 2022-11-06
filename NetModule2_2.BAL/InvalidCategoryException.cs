using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.BAL
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException(string message) : base(message) { }
    }
}

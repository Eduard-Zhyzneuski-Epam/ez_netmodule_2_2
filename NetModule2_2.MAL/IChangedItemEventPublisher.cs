using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.EAL
{
    public interface IChangedItemEventPublisher
    {
        public void Publish(ChangedItem changedItem);
    }
}

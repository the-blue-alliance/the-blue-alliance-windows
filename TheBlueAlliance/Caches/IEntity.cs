using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBA.Caches
{
    public interface IEntity<TKey>
    {
        TKey Key { get; set; }
    }
}

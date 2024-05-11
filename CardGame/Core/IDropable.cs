using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public interface IDropable
    {
        bool OnDrop(IDragable dropped, int x, int y);
    }
}

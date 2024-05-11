using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core
{
    public interface IClickable
    {
        bool IsClicked(int x, int y);

        public IClickable OnClick(int x, int y);

        //public void OnDrag(int x, int y);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameElements.GameCards
{
    public class Aura
    { 
        protected string Keyword { get; set; }
    }

    public class ModifyAttackAura : Aura
    {
        public int Modifier { get; private set; }

        public ModifyAttackAura(int value)
        {
            Modifier = value;
            Keyword = null;
        }
    }

    public class ModifyHealthAura : Aura
    {
        public int Modifier { get; private set; }

        public ModifyHealthAura(int value)
        {
            Modifier = value;
            Keyword = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameState
{
    public enum TurnState
    {
        Start = 0,
        Refresh = 1,
        Sacrifice = 2,
        Deployment = 3,
        Attack = 4,
        Draw = 5,
        Cleanup = 6,
        End = 7
    }
}

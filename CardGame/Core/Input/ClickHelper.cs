using System.Collections.Generic;

namespace CardGame.Core.Input
{
    public class ClickHelper<T> where T : IClickable
    {
        public ClickHelper()
        {
        }

        public IClickable CheckClick(IEnumerable<T> validTargets, int x, int y)
        {
            foreach (var target in validTargets)
            {
                if (target.IsClicked(x, y))
                {
                    return target;
                }
            }

            return null;
        }
    }
}

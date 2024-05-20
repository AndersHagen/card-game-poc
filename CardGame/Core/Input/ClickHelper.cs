using System.Collections.Generic;

namespace CardGame.Core.Input
{
    public class ClickHelper
    {
        public List<IClickable> ValidTargets { get; private set; }

        public ClickHelper(List<IClickable> validTargets = null)
        {
            ValidTargets = validTargets ?? new List<IClickable>();
        }

        public IClickable CheckClick(IEnumerable<IClickable> validTargets, int x, int y)
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

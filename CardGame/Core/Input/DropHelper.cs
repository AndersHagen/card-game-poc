using System.Collections.Generic;

namespace CardGame.Core.Input
{
    public class DropHelper
    {
        public List<IDropable> ValidTargets { get; private set; }

        public DropHelper(List<IDropable> validTargets = null)
        {
            ValidTargets = validTargets ?? new List<IDropable>();
        }

        public IDropable CheckDrop(IEnumerable<IDropable> validTargets, int x, int y)
        {
            foreach (var target in validTargets)
            {
                if (target.Bound.Contains(x, y))
                {
                    return target;
                }
            }

            return null;
        }
    }
}

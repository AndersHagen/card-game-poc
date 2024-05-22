using System.Collections.Generic;

namespace CardGame.Core.Input
{
    public class DropHelper<T> where T : IDropable
    {
        public DropHelper(List<IDropable> validTargets = null)
        {
        }

        public IDropable CheckDrop(IEnumerable<T> validTargets, int x, int y)
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

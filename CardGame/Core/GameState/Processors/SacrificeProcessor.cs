using CardGame.Core.GameElements;
using CardGame.Core.Input.Commands;
using System.Collections.Generic;

namespace CardGame.Core.GameState.Processors
{
    public class SacrificeProcessor
    {
        public ActiveCard HeldCard => _dragHelper.HeldCard;

        private DragAndDropHelper<CardStack, DeadPile> _dragHelper;

        public SacrificeProcessor(Player player)
        {
            _dragHelper = new DragAndDropHelper<CardStack, DeadPile>(player.Hand.Stacks, new List<DeadPile> { player.DeadPile });
        }

        public bool ProcessSacrifice(List<GameCommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is EndStepCommand)
                {
                    return true;
                }
            }

            return _dragHelper.HandleDragAndDrop(commands);
        }
    }
}

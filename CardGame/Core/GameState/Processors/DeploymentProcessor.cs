using CardGame.Core.GameElements;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Core.GameState.Processors
{
    public class DeploymentProcessor
    {
        public ActiveCard HeldCard => _dragHelper.HeldCard;

        private DragAndDropHelper<CardStack, CardStack> _dragHelper;

        public DeploymentProcessor(Player player) 
        {
            _dragHelper = new DragAndDropHelper<CardStack, CardStack>(player.Hand.Stacks, player.GameArea.Stacks);
        }

        public bool ProcessDeployment(Player player, List<GameCommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is EndStepCommand)
                {
                    return true;
                }
            }
            
            _dragHelper.HandleDragAndDrop(commands);

            return false;
        }
    }
}

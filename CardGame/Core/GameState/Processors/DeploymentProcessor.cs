using CardGame.Core.GameElements;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Core.GameState.Processors
{
    public class DeploymentProcessor
    {
        private CardStack _sourceStack;

        private ClickHelper _clickHelper;
        private DropHelper _dropHelper;

        private bool _mousePressed;

        public HeldCard HeldCard;

        public DeploymentProcessor() 
        {
            _clickHelper = new ClickHelper();
            _dropHelper = new DropHelper();
            _mousePressed = false;
        }

        public bool ProcessDeployment(Player player, List<GameCommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is EndStepCommand)
                {
                    return true;
                }

                if (command is MouseClickCommand click)
                {
                    var clickedStack = _clickHelper.CheckClick(player.Hand.Stacks, click.X, click.Y);

                    if (clickedStack is CardStack stack)
                    {
                        var topCard = stack.PeekTopCard();

                        if (topCard != null && HeldCard == null)
                        {
                            HeldCard = new HeldCard
                            {
                                Texture = topCard.Lift(),
                                Center = topCard.Center,
                                Position = topCard.Position,
                                Scale = topCard.Scale + 0.02f
                            };
                            _sourceStack = stack;
                        }
                    }
                }
                if (command is MouseDraggedCommand mdc)
                {
                    if (!_mousePressed)
                    {
                        _mousePressed = true;
                    }

                    if (HeldCard != null)
                    {
                        HeldCard.Position = new Vector2(mdc.X, mdc.Y);
                    }

                }
                if (command is MouseReleaseCommand cmd)
                {
                    _mousePressed = false;

                    if (HeldCard != null)
                    {
                        var target = _dropHelper.CheckDrop(player.GameArea.Stacks, cmd.X, cmd.Y);

                        _sourceStack.TopCard.Release();

                        if (target is CardStack slot && slot.Available())
                        {
                            slot.AddCard(_sourceStack.TopCard);
                            _sourceStack.PopCard();
                        }

                        HeldCard = null;
                        _sourceStack = null;
                    }
                }

            }
            return false;
        }
    }
}

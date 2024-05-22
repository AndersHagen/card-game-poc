using CardGame.Core.GameElements;
using CardGame.Core.Input;
using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.GameState.Processors
{
    public class DragAndDropHelper<TPick, TDrop> where TPick : IClickable where TDrop : IDropable
    {
        public ActiveCard HeldCard { get; private set; }

        private List<TPick> _pickupTargets;
        private List<TDrop> _dropTargets;

        private ClickHelper<TPick> _clickHelper;
        private DropHelper<TDrop> _dropHelper;

        private CardStack _sourceStack;

        private bool _mousePressed;

        public DragAndDropHelper(List<TPick> pickupTargets, List<TDrop> dropables) 
        {
            _pickupTargets = pickupTargets;
            _dropTargets = dropables;
            _clickHelper = new ClickHelper<TPick>();
            _dropHelper = new DropHelper<TDrop>();
        }

        public bool HandleDragAndDrop(List<GameCommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is MouseClickCommand click)
                {
                    var clickedStack = _clickHelper.CheckClick(_pickupTargets, click.X, click.Y);

                    if (clickedStack is CardStack stack)
                    {
                        var topCard = stack.PeekTopCard();

                        if (topCard != null && HeldCard == null)
                        {
                            HeldCard = new ActiveCard
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
                        var result = false;
                        var target = _dropHelper.CheckDrop(_dropTargets, cmd.X, cmd.Y);

                        _sourceStack.TopCard.Release();

                        if (target is IDropable slot && slot.Available())
                        {
                            slot.AddCard(_sourceStack.TopCard);
                            _sourceStack.PopCard();
                            result = true;
                        }

                        HeldCard = null;
                        _sourceStack = null;

                        return result;
                    }
                }
            }

            return false;
        }
    }
}

using CardGame.Core.Input.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.Input
{
    public class InputHandler
    {
        private MouseState _previousState;
        private MouseState _currentState;

        public InputHandler() 
        {
        }

        public List<GameCommand> GetInput()
        {
            var commands = new List<GameCommand>();

            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                return new List<GameCommand> { new ExitCommand() };
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                return new List<GameCommand> { new EndStepCommand() };
            }

            _currentState = Mouse.GetState();

            if (HasBeenClicked())
            {
                commands.Add(new MouseClickCommand(_currentState.X, _currentState.Y));
            }

            if (HasBeenReleased())
            {
                commands.Add(new MouseReleaseCommand(_currentState.X, _currentState.Y));
            }

            if (HasBeenHeld())
            {
                commands.Add(new MouseDraggedCommand(_currentState.X, _currentState.Y));
            }

            _previousState = _currentState;

            return commands;
        }

        private bool HasBeenClicked(bool isLeftButton = true)
        {
            var button = isLeftButton ? _currentState.LeftButton : _currentState.RightButton;
            var previous = isLeftButton ? _previousState.LeftButton : _previousState.RightButton;

            return button == ButtonState.Pressed && previous == ButtonState.Released;
        }

        private bool HasBeenReleased(bool isLeftButton = true)
        {
            var button = isLeftButton ? _currentState.LeftButton : _currentState.RightButton;
            var previous = isLeftButton ? _previousState.LeftButton : _previousState.RightButton;

            return button == ButtonState.Released && previous == ButtonState.Pressed;
        }

        private bool HasBeenHeld(bool isLeftButton = true)
        {
            var button = isLeftButton ? _currentState.LeftButton : _currentState.RightButton;
            var previous = isLeftButton ? _previousState.LeftButton : _previousState.RightButton;

            return button == ButtonState.Pressed && previous == ButtonState.Pressed;
        }
    }
}

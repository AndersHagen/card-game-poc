using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.Input.Commands
{
    public abstract class GameCommand
    {
    }

    public class EmptyCommand : GameCommand { }

    public class ExitCommand : GameCommand { }

    public class DrawCommand : GameCommand { }

    public class MouseClickCommand : GameCommand 
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public MouseClickCommand(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Mouse clicked at: ({X}, {Y})";
        }
    }

    public class MouseReleaseCommand : GameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MouseReleaseCommand(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Mouse released at: ({X}, {Y})";
        }
    }

    public class MouseDraggedCommand : GameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MouseDraggedCommand(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Mouse dragged to: ({X}, {Y})";
        }
    }
}

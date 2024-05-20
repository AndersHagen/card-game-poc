using System;
using System.Diagnostics;

namespace CardGame.Core.GameState
{
    public class TurnManager
    {
        public TurnState CurrentState { get; private set; }

        public int CurrentTurn { get; private set; }

        public TurnManager()
        {
            CurrentState = TurnState.Start;
            CurrentTurn = 1;
        }

        public void ProgressToNextState()
        {
            var next = ((int)CurrentState + 1) % Enum.GetValues(typeof(TurnState)).Length;

            Debug.WriteLine($"- Phase-transiation '{CurrentState}' -> '{(TurnState)next}'");

            CurrentState = (TurnState)next;
        }
    }
}

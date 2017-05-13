using UserManagement;

namespace GameLogic
{
    public class Turn
    {
        private ActionType _action;
        private Chip _chipsUsedInRound;
        private Player _player;
        public enum ActionType
        {
            Fold,
            Check,
            AllIn,
            Call,
            Raise
        }
        public Turn(Player player,ActionType action, Chip ChipsUsedInRound)
        {
            this._chipsUsedInRound = ChipsUsedInRound;
            this._player = player;
            this._action = action;
        }
    }
}

namespace GameLogic
{
    public interface IDealer
    {
        bool AllIn(Player player);
        bool Raise(Player player, Chip chip);
        bool Call(Player player);
        bool Check(Player player);
        bool Fold(Player player);
        bool LeaveToLobby(Player player);
    }
}

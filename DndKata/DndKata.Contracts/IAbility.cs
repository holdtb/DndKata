namespace DndKata.Contracts
{
    public interface IAbility
    {
        int Score { set; }
        int Modifier { get; }
    }
}
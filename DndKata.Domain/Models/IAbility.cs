namespace DndKata.Domain.Models
{
    public interface IAbility
    {
        AbilityType Ability { get; set; }
        int Score { set; }
        int Modifier { get; }
    }
}
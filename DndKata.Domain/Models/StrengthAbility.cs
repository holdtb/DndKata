namespace DndKata.Domain.Models
{
    public class StrengthAbility : IAbility
    {
        public AbilityType Ability { get; set; }
        public int Score { get; set; }
        public int Modifier => ModifierTable.GetModifierTable()[Score];

        public StrengthAbility()
        {
            Ability = AbilityType.Strength;
            Score = 10;
        }
    }
}
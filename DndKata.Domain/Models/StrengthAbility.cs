using DndKata.Contracts;

namespace DndKata.Domain.Models
{
    public class StrengthAbility : IAbility
    {
        public int Score { get; set; }
        public int Modifier => ModifierTable.GetModifierTable()[Score];

        public StrengthAbility()
        {
            Score = 10;
        }
    }
}
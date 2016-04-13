using DndKata.Contracts;

namespace DndKata.Domain.Models
{
    public class RollCalculator : IRollCalculator
    {
        public int GetEnhancedRoll(int roll, StrengthModifierResult strengthModifierResult)
        {
            return roll + strengthModifierResult.Modifier;
        }
    }
}
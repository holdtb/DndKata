namespace DndKata.Contracts
{
    public interface IRollCalculator
    {
        int GetEnhancedRoll(int roll, StrengthModifierResult strengthModifierResult);
    }
}
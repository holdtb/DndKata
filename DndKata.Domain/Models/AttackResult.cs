namespace DndKata.Domain.Models
{
    public class AttackResult
    {
        public bool WasHit { get; set; }

        public int DamageDealt { get; set; }
        public bool LethalHit { get; set; }

    }
}
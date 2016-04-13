using System.Collections.Generic;
using System.Globalization;

namespace DndKata.Domain.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public int HealthPoints { get; set; }
        public List<IAbility> Abilities { get; set; } 

        public Character()
        {
            Name = "Default";
            Armor = 10;
            HealthPoints = 5;
            Abilities = new List<IAbility>();
        }

        public AttackResult Attack(Character opponent, int roll)
        {
            if (roll >= opponent.Armor)
            {
                var damageDealt = roll == 20 ? 2 : 1;

                opponent.HealthPoints -= damageDealt;

                return new AttackResult()
                {
                    DamageDealt = damageDealt,
                    WasHit = true,
                    LethalHit = opponent.HealthPoints <= 0
                };
            }

            return new AttackResult()
            {
                DamageDealt = 0,
                WasHit = false,
                LethalHit = false
            };
        }
    }
}
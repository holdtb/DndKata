using System.Collections.Generic;
using DndKata.Contracts;
using DndKata.Extensions;

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
            var strengthModifier = GetStrengthModifier(Abilities);
            var enhancedRoll = roll + strengthModifier;

            if (enhancedRoll < opponent.Armor)
            {
                return new AttackResult
                {
                    DamageDealt = 0,
                    WasHit = false,
                    LethalHit = false
                };
            }

            var baseDamageDealt = GetBaseDamage(roll);
            InflictDamage(opponent, baseDamageDealt, strengthModifier);

            return new AttackResult()
            {
                DamageDealt = baseDamageDealt + strengthModifier,
                WasHit = true,
                LethalHit = opponent.HealthPoints <= 0
            };
        }

        private static int GetBaseDamage(int roll)
        {
            return roll == 20 ? 2 : 1;
        }

        private static void InflictDamage(Character opponent, int baseDamageDealt, int strengthModifier)
        {
            opponent.HealthPoints -= baseDamageDealt + strengthModifier;
        }

        private int GetStrengthModifier(List<IAbility> abilities)
        {
            if (!abilities.ContainsAbility(typeof(StrengthAbility))) return 0;

            var strengthAbility = Abilities.Find(a => a.GetType() == typeof(StrengthAbility));
            return strengthAbility.Modifier;
        }
    }
}
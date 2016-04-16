using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            var strengthModifierResult = GetStrengthModifier(Abilities);
            var enhancedRoll = roll + strengthModifierResult.Modifier;

            if (strengthModifierResult.AbilityPresent && enhancedRoll < opponent.Armor)
            {
                return new AttackResult
                {
                    DamageDealt = 1,
                    WasHit = true,
                    LethalHit = false
                };
            }
            if (!strengthModifierResult.AbilityPresent && enhancedRoll < opponent.Armor)
            {
                return new AttackResult
                {
                    DamageDealt = 0,
                    WasHit = false,
                    LethalHit = false
                };
            }

            var baseDamageDealt = GetBaseDamage(roll);
            InflictDamage(opponent, baseDamageDealt, strengthModifierResult.Modifier);

            return new AttackResult
            {
                DamageDealt = baseDamageDealt + strengthModifierResult.Modifier,
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

        private StrengthModifierResult GetStrengthModifier(List<IAbility> abilities)
        {
            if (!abilities.ContainsAbility(typeof(StrengthAbility))) return new StrengthModifierResult()
            {
                AbilityPresent = false,
                Modifier = 0
            };

            var strengthAbility = Abilities.Find(a => a.GetType() == typeof(StrengthAbility));
            return new StrengthModifierResult()
            {
                Modifier = strengthAbility.Modifier,
                AbilityPresent = true
            };
        }
    }
}
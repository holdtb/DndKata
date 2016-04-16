using System.Collections.Generic;
using DndKata.Contracts;
using DndKata.Domain.Models;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace DndKata.Domain.Unit.Tests
{
    [TestFixture]
    public class StrengthAbilityTests
    {

        [SetUp]
        public void BeforeEach()
        {
        }

        [Test]
        public void DefaultScoreOf10()
        {
            // Arrange
            var ability = new StrengthAbility();

            // Act

            // Assert
            Assert.That(ability.Score, Is.EqualTo(10));
        }

        [Test]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(1)]
        public void CharacterAttacks_HasStrengthModifier_MinimumDamageInflictedIs1(int roll)
        {
            // Arrange
            const int numLargerThanPossibleRoll = 50;
            var character = new Character
            {
                Abilities = new List<IAbility>
                {
                    new StrengthAbility{Score = 1}
                }
            };
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = numLargerThanPossibleRoll)
                .Do(o => o.HealthPoints = numLargerThanPossibleRoll)
                .Build();

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(1));
        }

        [Test]
        public void ChracterAttacks_CriticalAttack_HasStrengthAbility_ModifierAddedToRollAndDamageDealt()
        {
            // Arrange
            const int initialOpponentHealth = 1;
            var character = new Character
            {
                Abilities = new List<IAbility>
                {
                    new StrengthAbility{Score = 20}
                }
            };
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 14)
                .Do(o => o.HealthPoints = initialOpponentHealth)
                .Build();
            const int roll = 20;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(7));
        }

        [Test]
        public void ChracterAttacks_NormalAttack_HasStrengthAbility_ModifierAddedToRollAndDamageDealt()
        {
            // Arrange
            const int initialOpponentHealth = 1;
            var character = new Character
            {
                Abilities = new List<IAbility>
                {
                    new StrengthAbility{Score = 20}
                }
            };
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 14)
                .Do(o => o.HealthPoints = initialOpponentHealth)
                .Build();
            const int roll = 10;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(6));
        }

        [Test]
        public void StrengthAbility_EnhancesRoll()
        {
            // Arrange
            const int initialOpponentHealth = 1;
            var character = new Character
            {
                Abilities = new List<IAbility>
                {
                    new StrengthAbility{Score = 20}
                }
            };

            var opponent = new Character
            {
                Armor = 2,
                HealthPoints = initialOpponentHealth
            };
            
            const int roll = 1;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit);
            Assert.That(attackResult.LethalHit);
        }
    }
}

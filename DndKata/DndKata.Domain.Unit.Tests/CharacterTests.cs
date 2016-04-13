using System.Collections.Generic;
using DndKata.Contracts;
using DndKata.Domain.Models;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace DndKata.Domain.Unit.Tests
{
    [TestFixture]
    public class CharacterTests
    {
        private RollCalculator _rollCalculator;

        [SetUp]
        public void Setup()
        {
            _rollCalculator = new RollCalculator();
        }

        [Test]
        public void NewCharacter_SetsCorrectDefaults()
        {
            // Arrange
            var character = new Character(_rollCalculator);

            // Act
            // Assert
            Assert.That(character.Name, Is.EqualTo("Default"));
            Assert.That(character.Armor, Is.EqualTo(10));
            Assert.That(character.HealthPoints, Is.EqualTo(5));
            Assert.That(character.Abilities, Is.EqualTo(new List<IAbility>()));
        }

        [Test]
        public void Character_CanAttack()
        {
            // Arrange
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>.CreateNew().Build();
            const int roll = 5;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult, Is.Not.Null);
        }

        [Test]
        public void Attack_RollLessThanOpponentArmor_DoesNotHit()
        {
            // Arrange
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>
                .CreateNew().Do(o => o.Armor = 2).Build();
            var roll = opponent.Armor - 1;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.False);
        }

        [Test]
        public void Attack_RollHigherThanOpponentArmor_Hits()
        {
            // Arrange
            const int initialOpponentHealth = 5;
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 2)
                .Do(o => o.HealthPoints = initialOpponentHealth)
                .Build();
            var roll = opponent.Armor + 1;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(1));
            Assert.That(opponent.HealthPoints, Is.LessThan(initialOpponentHealth));
        }

        [Test]
        public void Attack_RollEqualToOpponentArmor_Hits()
        {
            // Arrange
            const int initialOpponentHealth = 5;
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 2)
                .Do(o => o.HealthPoints = initialOpponentHealth)
                .Build();
            var roll = opponent.Armor;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(1));
            Assert.That(opponent.HealthPoints, Is.LessThan(initialOpponentHealth));
        }

        [Test]
        public void NaturalTwentyRolled_CriticalHitAttack_DoublesDamage()
        {
            // Arrange
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 5)
                .Do(o => o.HealthPoints = 5)
                .Build();
            const int roll = 20;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(attackResult.WasHit, Is.True);
            Assert.That(attackResult.DamageDealt, Is.EqualTo(2));
            Assert.That(attackResult.LethalHit, Is.False);
        }

        [Test]
        public void CharacterAttacksOpponent_OpponentHitpointsZero_OpponentDead()
        {
            // Arrange
            const int initialOpponentHealth = 1;
            var character = new Character(_rollCalculator);
            var opponent = Builder<Character>
                .CreateNew()
                .Do(o => o.Armor = 2)
                .Do(o => o.HealthPoints = initialOpponentHealth)
                .Build();
            var roll = opponent.Armor;

            // Act
            var attackResult = character.Attack(opponent, roll);

            // Assert
            Assert.That(opponent.HealthPoints, Is.LessThan(initialOpponentHealth));
            Assert.That(attackResult.LethalHit, Is.True);
        }   
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DndKata.Domain.Models;
using NUnit.Framework;

namespace DndKata.Domain.Unit.Tests
{
    [TestFixture]
    public class StrengthAbilityTests
    {
        [Test]
        public void DefaultScoreOf10()
        {
            // Arrange
            var ability = new StrengthAbility();

            // Act

            // Assert
            Assert.That(ability.Score, Is.EqualTo(10));
        }
    }
}

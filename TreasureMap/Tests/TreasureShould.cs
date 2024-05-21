using TreasureMap.Models;
using Xunit;

namespace TreasureMap.Tests
{
    public class TreasureShould
    {
        [Fact]
        public void Explore_AdventurerTakesOneTreasure_WhenTreasureCountNotEqualToZeroOrLess()
        {
            // Given
            var adventurer = new Adventurer("Lara", new Position(1, 2), Orientation.North, new Queue<Movement>(), 3);
            var treasure = new Treasure(1);
            var expectedTreasureCount = treasure.TreasureCount - 1;
            var expectedAdventurerTreasureCount = adventurer.TreasureCount + 1;

            // When
            treasure.Collect(adventurer);

            // Then
            Assert.Equal(expectedTreasureCount, treasure.TreasureCount);
            Assert.Equal(expectedAdventurerTreasureCount, adventurer.TreasureCount);
        }

        [Fact]
        public void Explore_AdventurerTakesZeroTreasure_WhenTreasureCountEqualToZeroOrLess()
        {
            // Given
            var adventurer = new Adventurer("Lara", new Position(1, 2), Orientation.North, new Queue<Movement>(), 3);
            var treasure = new Treasure(0);
            var expectedTreasureCount = treasure.TreasureCount;
            var expectedAdventurerTreasureCount = adventurer.TreasureCount;

            // When
            treasure.Collect(adventurer);

            // Then
            Assert.Equal(expectedTreasureCount, treasure.TreasureCount);
            Assert.Equal(expectedAdventurerTreasureCount, adventurer.TreasureCount);
        }
    }
}

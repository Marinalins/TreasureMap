using TreasureMap.Models;
using Xunit;

namespace TreasureMap.Tests
{
    public class MapShould
    {
        [InlineData(-1, 3)]
        [InlineData(2, -2)]
        [InlineData(4, 4)]
        [InlineData(3, 6)]
        [Theory]
        public void ExploreCase_ReturnsFalse_WhenPositionNotWithinMapBoundaries(int x, int y)
        {
            // Given
            var position = new Position(x, y);
            var map = new Map(4, 6, new Dictionary<Position, ICase>());
            var adventurer = new Adventurer("Lara", new Position(1, 2), Orientation.North, new Queue<Movement>(), 3);

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            Assert.False(result);
        }

        [Fact]
        public void ExploreCase_ReturnsFalse_WhenCaseIsNotCrossable()
        {
            // Given
            var position = new Position(1, 2);
            var cases = new Dictionary<Position, ICase>()
            {
                { position, new Mountain() }
            };
            var map = new Map(4, 6, cases);
            var adventurer = new Adventurer("Lara", new Position(2, 2), Orientation.North, new Queue<Movement>(), 3);

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            Assert.False(result);
        }

        [Fact]
        public void ExploreCase_ReturnsTrue_WhenCaseIsCrossable()
        {
            // Given
            var position = new Position(1, 2);
            var cases = new Dictionary<Position, ICase>()
            {
                { position, new Treasure(1) }
            };
            var map = new Map(4, 6, cases);
            var adventurer = new Adventurer("Lara", new Position(2, 2), Orientation.North, new Queue<Movement>(), 3);

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            Assert.True(result);
        }

        [Fact]
        public void ExploreCase_ReturnsTrue_WhenNoObstacle()
        {
            // Given
            var position = new Position(1, 2);
            var map = new Map(4, 6, new Dictionary<Position, ICase>());
            var adventurer = new Adventurer("Lara", new Position(2, 2), Orientation.North, new Queue<Movement>(), 3);

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            Assert.True(result);
        }


        [Fact]
        public void ExploreCase_CollectAndReturnsTrue_WhenCaseIsCollectable()
        {
            // Given
            var position = new Position(1, 2);
            var treasure = new Treasure(2);
            var cases = new Dictionary<Position, ICase>()
            {
                { position, treasure }
            };
            var map = new Map(4, 6, cases);
            var adventurer = new Adventurer("Lara", new Position(2, 2), Orientation.North, new Queue<Movement>(), 3);
            var expectedTreasureCount = treasure.TreasureCount - 1;
            var expectedAdventurerTreasureCount = adventurer.TreasureCount + 1;

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            Assert.True(result);
            Assert.Equal(expectedTreasureCount, treasure.TreasureCount);
            Assert.Equal(expectedAdventurerTreasureCount, adventurer.TreasureCount);
        }

        [Fact]
        public void ExploreCase_RemoveCollectableAndReturnsTrue_WhenCaseIsCollectableAndAlreadyCollected()
        {
            // Given
            var position = new Position(1, 2);
            var treasure = new Treasure(0);
            var cases = new Dictionary<Position, ICase>()
            {
                { position, treasure }
            };
            var map = new Map(4, 6, cases);
            var adventurer = new Adventurer("Lara", new Position(2, 2), Orientation.North, new Queue<Movement>(), 3);

            // When
            var result = map.ExploreCase(position, adventurer);

            // Then
            cases.TryGetValue(position, out var @case);
            Assert.Null(@case);
            Assert.True(result);
        }
    }
}

using TreasureMap.Models;
using Xunit;

namespace TreasureMap.Tests
{
    public class AdventurerShould
    {
        [Fact]
        public void TakeTresor_AddsOneTresor()
        {
            // Given
            var adventurer = new Adventurer("Lara", new Position(2, 3), Orientation.North, new Queue<Movement>(), 3);
            var expectedTresorCount = adventurer.TreasureCount + 1;

            // When
            adventurer.TakeTresure();

            // Then
            Assert.Equal(expectedTresorCount, adventurer.TreasureCount);
        }

        [Fact]
        public void IsNextMovementChangingPosition_ReturnsTrue_WhenNextMovementIsForward()
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Forward);
            movements.Enqueue(Movement.Right);
            var adventurer = new Adventurer("Lara", new Position(2, 3), Orientation.North, movements, 3);
            var expectedNextPosition = new Position(adventurer.Position.X, adventurer.Position.Y - 1);

            // When
            var result = adventurer.IsNextMovementChangingPosition(out var nextPosition);

            // Then
            Assert.True(result);
            Assert.Equal(expectedNextPosition, nextPosition);
        }

        [Fact]
        public void IsNextMovementChangingPosition_ReturnsFalse_WhenNextMovementIsNotForward()
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Right);
            movements.Enqueue(Movement.Forward);
            var adventurer = new Adventurer("Lara", new Position(2, 3), Orientation.North, movements, 3);
            var expectedNextPosition = adventurer.Position;

            // When
            var result = adventurer.IsNextMovementChangingPosition(out var nextPosition);

            // Then
            Assert.False(result);
            Assert.Equal(expectedNextPosition, nextPosition);
        }

        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.West, Orientation.North)]
        [Theory]
        public void ExecuteNextMovement_OrientationChanges_WhenNextMovementIsRight(Orientation orientation, Orientation expectedOrientation)
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Right);
            movements.Enqueue(Movement.Forward);
            var adventurer = new Adventurer("Lara", new Position(2, 3), orientation, movements, 3);
            var expectedPosition = adventurer.Position;

            // When
            adventurer.ExecuteNextMovement(false);

            // Then
            Assert.Equal(expectedPosition, adventurer.Position);
            Assert.Equal(expectedOrientation, adventurer.Orientation);
            Assert.Single(adventurer.Movements);
        }

        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.West, Orientation.South)]
        [Theory]
        public void ExecuteNextMovement_OrientationChanges_WhenNextMovementIsLeft(Orientation orientation, Orientation expectedOrientation)
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Left);
            movements.Enqueue(Movement.Forward);
            var adventurer = new Adventurer("Lara", new Position(2, 3), orientation, movements, 3);
            var expectedPosition = adventurer.Position;

            // When
            adventurer.ExecuteNextMovement(false);

            // Then
            Assert.Equal(expectedPosition, adventurer.Position);
            Assert.Equal(expectedOrientation, adventurer.Orientation);
            Assert.Single(adventurer.Movements);
        }

        [InlineData(Orientation.North, 2, 0, 2 ,-1)]
        [InlineData(Orientation.East, 2, 3, 3, 3)]
        [InlineData(Orientation.South, 5, 1, 5, 2)]
        [InlineData(Orientation.West, 0, 2, -1, 2)]
        [Theory]
        public void ExecuteNextMovement_PositionChanges_WhenNextMovementIsForwardAndAdventurerCanMove(Orientation orientation, int x, int y, int expectedX, int expectedY)
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Forward);
            movements.Enqueue(Movement.Left);
            var adventurer = new Adventurer("Lara", new Position(x, y), orientation, movements, 3);
            var expectedPosition = new Position(expectedX, expectedY);

            // When
            adventurer.ExecuteNextMovement(true);

            // Then
            Assert.Equal(expectedPosition, adventurer.Position);
            Assert.Equal(orientation, adventurer.Orientation);
            Assert.Single(adventurer.Movements);
        }

        [InlineData(Orientation.North, 2, 0)]
        [InlineData(Orientation.East, 2, 3)]
        [InlineData(Orientation.South, 5, 1)]
        [InlineData(Orientation.West, 0, 2)]
        [Theory]
        public void ExecuteNextMovement_PositionChanges_WhenNextMovementIsForwardAndAdventurerCanNotMove(Orientation orientation, int x, int y)
        {
            // Given
            var movements = new Queue<Movement>();
            movements.Enqueue(Movement.Forward);
            movements.Enqueue(Movement.Left);
            var adventurer = new Adventurer("Lara", new Position(x, y), orientation, movements, 3);
            var expectedPosition = adventurer.Position;

            // When
            adventurer.ExecuteNextMovement(false);

            // Then
            Assert.Equal(expectedPosition, adventurer.Position);
            Assert.Equal(orientation, adventurer.Orientation);
            Assert.Single(adventurer.Movements);
        }
    }
}

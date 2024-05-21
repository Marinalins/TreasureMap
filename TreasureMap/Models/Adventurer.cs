namespace TreasureMap.Models
{
    internal class Adventurer
    {
        public string Name { get; }
        public Position Position { get; private set; }
        public Orientation Orientation { get; private set; }
        public Queue<Movement> Movements { get; private set; }
        public int TreasureCount { get; private set; }

        public Adventurer(string name, Position position, Orientation orientation, Queue<Movement> movements, int treasureCount)
        {
            Name = name;
            Position = position;
            Orientation = orientation;
            Movements = movements;
            TreasureCount= treasureCount;
        }

        public void TakeTresure()
        {
            TreasureCount++;
        }

        public bool IsNextMovementChangingPosition(out Position nextPosition)
        {
            var nextMove = Movements.Peek();

            nextPosition = nextMove == Movement.Forward ? MoveForward(Position, Orientation) : Position;

            return nextMove == Movement.Forward;
        }

        public void ExecuteNextMovement(bool canMove)
        {
            var movement = Movements.Dequeue();

            switch (movement)
            {
                case Movement.Right:
                    Orientation = TurnRight(Orientation);
                    break;
                case Movement.Left:
                    Orientation = TurnLeft(Orientation);
                    break;
                case Movement.Forward:
                    if (canMove)
                    {
                        Position = MoveForward(Position, Orientation);
                    }
                    break;
            }
        }

        private static Orientation TurnRight(Orientation orientation)
            => orientation switch
            {
                Orientation.North => Orientation.East,
                Orientation.East => Orientation.South,
                Orientation.South => Orientation.West,
                Orientation.West => Orientation.North,
                _ => throw new ArgumentException("Orientation invalide")
            };

        private static Orientation TurnLeft(Orientation orientation)
            => orientation switch
            {
                Orientation.North => Orientation.West,
                Orientation.East => Orientation.North,
                Orientation.South => Orientation.East,
                Orientation.West => Orientation.South,
                _ => throw new ArgumentException("Orientation invalide")
            };

        private static Position MoveForward(Position position, Orientation orientation)
            => orientation switch
            {
                Orientation.North => new Position(position.X, position.Y - 1),
                Orientation.East => new Position(position.X + 1, position.Y),
                Orientation.South => new Position(position.X, position.Y + 1),
                Orientation.West => new Position(position.X - 1, position.Y),
                _ => throw new ArgumentException("Orientation invalide")
            };

        public override string ToString()
        {
            var result = "A - " + Name + " - " + Position.X + " - " + Position.Y + " - " + (char)Orientation + " - " + TreasureCount;
            if(Movements.Count > 0)
            {
                result += " - " + string.Join("", Movements.Select(m => (char)m));
            }
            return result;
        }
    }
}

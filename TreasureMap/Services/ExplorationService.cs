using TreasureMap.Models;

namespace TreasureMap.Services
{
    internal class ExplorationService
    {
        private readonly Map _map;
        private readonly Adventurer _adventurer;

        public ExplorationService(Map map, Adventurer adventurer)
        {
            _map = map;
            _adventurer = adventurer;
        }

        public void Explore()
        {
            while(_adventurer.Movements.Count > 0) {
                var canMove = false;
                if(_adventurer.IsNextMovementChangingPosition(out var nextPosition))
                {
                    canMove = _map.ExploreCase(nextPosition, _adventurer);
                }
                _adventurer.ExecuteNextMovement(canMove);
            }
        }

        public override string ToString()
        {
            return _map.ToString() + "\n" + _adventurer.ToString();
        }
    }
}

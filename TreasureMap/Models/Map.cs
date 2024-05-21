namespace TreasureMap.Models
{
    internal class Map
    {
        public int Width { get; }
        public int Height { get; }
        public Dictionary<Position, ICase> Cases { get; }

        public Map(int width, int height, Dictionary<Position, ICase> cases)
        {
            Width = width;
            Height = height;
            Cases = cases;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>
        ///     true si l'exploration est un succès et le déplacement sur cette case est possible<br />
        ///     false si l'exploration est un échec et le déplacement sur cette case est impossible (montagne ou bord de la carte atteint)
        /// </returns>
        public bool ExploreCase(Position position, Adventurer adventurer)
        {
            if (!PositionWithinMapBoundaries(position)) return false;

            Cases.TryGetValue(position, out var exploredCase);

            if (exploredCase == null) return true;

            if(exploredCase is ICollectable collectable)
            {
                collectable.Collect(adventurer);
                if(collectable.AlreadyCollected)
                {
                    Cases.Remove(position);
                }
            }

            return exploredCase.IsCrossable;
        }

        private bool PositionWithinMapBoundaries(Position position)
            => position.X >= 0 
            && position.Y >= 0
            && position.X <= Width - 1
            && position.Y <= Height - 1;

        public override string ToString()
        {
            var mapString = "C - " + Width + " - " + Height + "\n";
            for(var i = 0; i < Cases.Count; i++ )
            {
                var c = Cases.ElementAt(i);
                mapString += String.Format(c.Value.ToString() ?? string.Empty, c.Key.X, c.Key.Y);
                if(i != Cases.Count - 1) mapString += "\n";
            }
            return mapString;
        }
    }
}

namespace TreasureMap.Models
{
    internal class Treasure : ICase, ICollectable
    {
        public bool IsCrossable => true;
        public int TreasureCount { get; private set; }
        public bool AlreadyCollected => TreasureCount <= 0;

        public Treasure(int treasureCount)
        {
            TreasureCount = treasureCount;
        }

        public void Collect(Adventurer adventurer)
        {
            if (TreasureCount > 0)
            {
                TreasureCount--;
                adventurer.TakeTresure();
            }
        }

        public override string ToString()
        {
            return "T - {0} - {1} - " + TreasureCount;
        }
    }
}

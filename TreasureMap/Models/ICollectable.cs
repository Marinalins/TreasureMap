namespace TreasureMap.Models
{
    internal interface ICollectable
    {
        bool AlreadyCollected { get; }
        void Collect(Adventurer adventurer);
    }
}
namespace TreasureMap.Models
{
    internal class Mountain : ICase
    {
        public bool IsCrossable => false;

        public override string ToString()
        {
            return "M - {0} - {1}";
        }
    }
}

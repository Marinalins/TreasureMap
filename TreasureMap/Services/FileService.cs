using TreasureMap.Models;

namespace TreasureMap.Services
{
    internal static class FileService
    {

        public static (Map Map, Adventurer Adventurer)? DeserializeFile(string path)
        {
            var file = ReadFile(path);

            if (file == null)
            {
                Console.WriteLine("Fichier introuvable");
                return null;
            }

            try
            {
                var mountains = file.Where(l => l.First().ToUpper().Equals("M"))
                                    .ToDictionary<IEnumerable<string>, Position, ICase>(
                                        l => new Position(int.Parse(l.ElementAt(1)), int.Parse(l.ElementAt(2))),
                                        l => new Mountain());

                var treasures = file.Where(l => l.First().ToUpper().Equals("T"))
                                    .ToDictionary<IEnumerable<string>, Position, ICase>(
                                        l => new Position(int.Parse(l.ElementAt(1)), int.Parse(l.ElementAt(2))),
                                        l => new Treasure(int.Parse(l.ElementAt(3))));

                var cases = mountains.Union(treasures).ToDictionary();

                var mapString = file.Single(l => l.First().ToUpper().Equals("C"));
                var map = new Map(int.Parse(mapString.ElementAt(1)), int.Parse(mapString.ElementAt(2)), cases);

                var adventurerString = file.Single(l => l.First().ToUpper().Equals("A"));
                var adventurer = new Adventurer(
                    adventurerString.ElementAt(1),
                    new Position(int.Parse(adventurerString.ElementAt(2)), int.Parse(adventurerString.ElementAt(3))),
                    (Orientation)adventurerString.ElementAt(4).Single(),
                    MapToMovementQueue(adventurerString.ElementAt(5)),
                    0);

                return (map, adventurer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la conversion du fichier");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private static IEnumerable<IEnumerable<string>>? ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path)
                           .ToList()
                           .Select(line => line.Split('-').Select(i => i.Trim()));
            }
            return null;
        }

        private static Queue<Movement> MapToMovementQueue(string movements)
        {
            var queue = new Queue<Movement>();
            foreach(var chara in movements)
            {
                queue.Enqueue((Movement)chara);
            }
            return queue;
        }
    }
}

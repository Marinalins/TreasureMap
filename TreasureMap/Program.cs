using TreasureMap.Services;

Console.WriteLine("---------------LECTURE DU FICHIER D'ENTREE---------------");
var inputPath = ".\\Files\\lara.txt";
Console.WriteLine("Lecture du fichier '" + inputPath + "' en cours...");
var file = FileService.DeserializeFile(inputPath);
Console.WriteLine("Lecture terminée.");

if (file != null )
{
    var service = new ExplorationService(file.Value.Map, file.Value.Adventurer);
    Console.WriteLine(service.ToString() + "\n");

    Console.WriteLine("--------SIMULATION DES MOUVEMENTS DES AVENTURIERS--------");
    Console.WriteLine("Exploration en cours...");
    service.Explore();
    Console.WriteLine("Exploration terminée.\n");

    Console.WriteLine("--------------ECRITURE DU FICHIER DE SORTIE--------------");
    var outputPath = ".\\Files\\lara_output.txt";
    Console.WriteLine("Ecriture du fichier '" + outputPath + "' en cours...");
    var outputContent = service.ToString();
    File.WriteAllText(outputPath, outputContent);
    Console.WriteLine("Ecriture terminée.");
    Console.WriteLine(outputContent);
}
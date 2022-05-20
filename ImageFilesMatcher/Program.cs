using Common;
using System.Text.Json;

var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "gamedata.json");
var jsonString = File.ReadAllText(jsonFilePath);
var gameData = JsonSerializer.Deserialize<GameData>(jsonString);
if (gameData is not null)
{
    var imageBaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "countryimages");
    int imageFilesExist = 0;
    int imageFilesDontExist = 0;
    foreach (var country in gameData.Countries)
    {
        var imageFilePath = Path.Combine(imageBaseDirectory, $"{country.Code}.svg");
        var exists = File.Exists(imageFilePath);
        if (exists)
        {
            imageFilesExist++;
        }
        else
        {
            Console.WriteLine($"Does not exist - {country.Code}: {country.Name}");
            imageFilesDontExist++;
        }
    }
    Console.WriteLine($"Files Exist: {imageFilesExist}");
    Console.WriteLine($"Files Don't Exist: {imageFilesDontExist}");
}

using Common;
using CsvHelper;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;

List<Continent>? continents;
var path = Path.Combine(Directory.GetCurrentDirectory(), "continents.csv");
using (var reader = new StreamReader(path))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    continents = csv.GetRecords<Continent>().ToList();
}

List<Country>? countries;
using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "countries.csv")))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    countries = csv.GetRecords<Country>().ToList();
}

foreach (var continent in continents)
{
    var countryCount = countries.Where(q => q.Continent == continent.Code).Count();
    Console.WriteLine($"{continent.Name}: {countryCount}");
}

var gameData = new GameData(countries, continents);

var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
serializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "gamedata.json"), JsonSerializer.Serialize(gameData, serializerOptions));
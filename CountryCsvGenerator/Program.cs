using CountryCsvGenerator;
using CsvHelper;
using System.Globalization;

var fileContents = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "index.html"));

int bodyTagIndex = 0;
int lastBodyTagIndex = 0;
for (int i = 0; i < fileContents.Length; i++)
{
    var line = fileContents[i];
    if (line.Contains("<tbody>"))
    {
        bodyTagIndex = i;
    }
    if (line.Contains("</tbody>"))
    {
        lastBodyTagIndex = i;
    }
}

var firstColumnTag = bodyTagIndex + 2;

var countryList = new List<Country>();
for (int i = firstColumnTag; i < lastBodyTagIndex; i += 15)
{
    var codeLine = fileContents[i];
    codeLine = codeLine.Trim();
    codeLine = codeLine.Replace("<td>", "");
    var code = codeLine.Replace("</td>", "");
    var nameLine = fileContents[i + 1];
    nameLine = nameLine.Trim();
    nameLine = nameLine.Replace("<td>", "");
    var name = nameLine.Replace("</td>", "");
    var country = new Country(code, name);
    countryList.Add(country);
}
using (var writer = new StreamWriter("countries.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(countryList);
};
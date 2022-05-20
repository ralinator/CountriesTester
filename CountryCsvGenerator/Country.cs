namespace CountryCsvGenerator;

internal class Country
{
    public Country(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; set; }
    public string Name { get; set; }
}

using CsvHelper;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RozetkaScrapper;

public class ExportResult
{
    private readonly JsonSerializerOptions _options =
       new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    private readonly static string _name = DateTime.Now.ToString("ff");
    private readonly string _jsonFormat = Environment.CurrentDirectory + $"\\results\\{_name}.json";
    private readonly string _csvFormat = Environment.CurrentDirectory + $"\\results\\{_name}.csv";

    public void SaveToCsv(IList<ProductModel> products)
    {
        using (var writer = new StreamWriter(_csvFormat))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(products);
        }
    }

    public  void SaveToJson(object obj)
    {
        //var options = new JsonSerializerOptions(_options)
        //{
        //    WriteIndented = true
        //};
        var jsonString = JsonSerializer.Serialize(obj);
        File.WriteAllText(_jsonFormat, jsonString);
    }
}

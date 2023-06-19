using CsvHelper;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RozetkaScrapper;

public class ExportResult
{
    private readonly JsonSerializerOptions _options =
       new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public void ConvertToCsv(IList<ProductModel> products)
    {
        using (var writer = new StreamWriter(Environment.CurrentDirectory + "\\results\\result.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(products);
        }
    }

    public  void ConvertToJson(object obj, string fileName)
    {
        var options = new JsonSerializerOptions(_options)
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(fileName, jsonString);
    }
}

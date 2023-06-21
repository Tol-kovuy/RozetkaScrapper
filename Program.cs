using RozetkaScrapper;

public class Program
{
    public static void Main(string[] args)
    {
        var startScrapping = new Parser("https://rozetka.com.ua/");  
        var products = startScrapping.RunParsing();

        Console.WriteLine("Input '1' if You want to save result to .csv or '2' to .json");
        var saveChoose = Convert.ToInt32(Console.ReadLine());
        var result = new ExportResult();
        if (saveChoose == 1)
        {
            result.SaveToCsv(products);
        }
        else
        {
            result.SaveToJson(products);
        }
        Console.WriteLine("See your result in folder 'results' in root folder");
    }
}
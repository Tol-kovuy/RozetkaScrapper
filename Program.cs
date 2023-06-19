using RozetkaScrapper;

public class Program
{
    public static void Main(string[] args)
    {
        var startScrapping = new Parser("https://rozetka.com.ua/");
        startScrapping.RunParsing();
    }
}
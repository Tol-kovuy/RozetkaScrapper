namespace RozetkaScrapper;

public class Parser
{
    public string Uri { get; set; }

    public Parser(string uri)
    {
        Uri = uri;
    }

    public void RunParsing()
    {
        Console.WriteLine("\n---Insert please count of searching products(min 60)---");
        int count = Convert.ToInt32(Console.ReadLine());
        var category = new Category(count);
        category.GetCategories(Uri);


        Console.WriteLine("\n---Copy(Cntr+C) the Link of Category and Paste(Cntrl+V) to string below---");
        var selectedCategory = Console.ReadLine();

        if (selectedCategory != null)
        {
            category.GetSubCategories(selectedCategory);
        }
        Console.WriteLine("\n---Copy(Cntr+C) the Link of SubCategory and Paste(Cntrl+V) to string below---");
        var selectedSubCategory = Console.ReadLine();
        IList<ProductModel> products = new List<ProductModel>();
        if (selectedSubCategory != null)
        {
            products = category.GetProducts(selectedSubCategory);
        }
        var result = new ExportResult();
        result.ConvertToCsv(products);
        result.ConvertToJson(products, Environment.CurrentDirectory + "\\results\\result.json");
        Console.Clear();
        Console.WriteLine("See your result in folder 'results' in root folder");
    }
}

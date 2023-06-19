using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RozetkaScrapper;

public class Category
{
    private readonly string _pathToActualChromeDriver = Environment.CurrentDirectory + "\\driver\\";
    public int ProductCount { get; set; }

    public Category(int productCount)
    {
        ProductCount = productCount;
    }

    public void GetCategories(string uri)
    {
        var driver = StartChromeDriver();
        driver.Navigate().GoToUrl(uri);
        Thread.Sleep(2000);
        var menuWrapper = driver.FindElement(By.ClassName("menu-wrapper"));
        Thread.Sleep(2000);
        var menuCategories = menuWrapper.FindElements(By.ClassName("menu-categories__item"));
        Thread.Sleep(2000);

        var links = menuCategories
            .Select(category => category.FindElement(By.ClassName("menu-categories__link")))
            .Select(link => link.GetAttribute("href"))
            .ToList();
        DisplayMenuLinks(links);
        driver.Close();
    }

    public void GetSubCategories(string categoriesUrl)
    {
        var driver = StartChromeDriver();
        driver.Navigate().GoToUrl(categoriesUrl);
        Thread.Sleep(2000);
        var menuWrapper = driver.FindElement(By.ClassName("portal-grid"));
        var menuCategories = menuWrapper.FindElements(By.ClassName("portal-grid__cell"));
        var links = menuCategories
            .Select(cat => cat.FindElement(By.ClassName("tile-cats")))
            .Select(tileCats => tileCats.FindElement(By.ClassName("tile-cats__heading")))
            .Select(href => href.GetAttribute("href"))
            .ToList();
        DisplayMenuLinks(links);
        driver.Close();
    }

    public IList<ProductModel> GetProducts(string subCategoriesUrl)
    {
        var driver = StartChromeDriver();
        driver.Navigate().GoToUrl(subCategoriesUrl);
        Thread.Sleep(2000);
        var products = new List<ProductModel>();
        var ngStarInserted = driver.FindElements(By.ClassName("catalog-grid__cell"));
        while (ngStarInserted.Count < ProductCount)
        {
            ShowMoreProducts(driver);
            Thread.Sleep(2000);
            ngStarInserted = driver.FindElements(By.ClassName("catalog-grid__cell"));
        }
        foreach (var item in ngStarInserted)
        {
            var goodTitleHeading = item.FindElement(By.ClassName("goods-tile__heading"));
            var title = goodTitleHeading.GetAttribute("title");
            var href = goodTitleHeading.GetAttribute("href");
            var prices = item.FindElement(By.ClassName("goods-tile__prices"));
            var price = prices.FindElement(By.ClassName("goods-tile__price")).Text;
            var product = new ProductModel
            {
                Description = title,
                Link = href,
                Price = price
            };
            products.Add(product);
        }
            driver.Close();
        return products;
    }

    public void ShowMoreProducts(IWebDriver driver)
    {
        var showMoreText = driver.FindElement(By.ClassName("show-more__text"));
        showMoreText.Click();
    }

    private IWebDriver StartChromeDriver()
    {
        IWebDriver driver = new ChromeDriver(_pathToActualChromeDriver);
        return driver;
    }

    private void DisplayMenuLinks(IList<string> links)
    {
        Console.Clear();
        foreach (var link in links)
        {
            Console.WriteLine(link);
        }
    }
}
